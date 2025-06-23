using AutoMapper;
using FuelGo.Dto;
using FuelGo.Inerfaces;
using FuelGo.Models;
using FuelGo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FuelGo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemAdminController : BaseController
    {
        private readonly IMapper _mapper;

        public SystemAdminController(UserInfoService userInfoService, IUnitOfWork unitOfWork, IMapper mapper) : 
            base(userInfoService, unitOfWork)
        {
            _mapper = mapper;
        }

        [HttpPost("add-Admin")]
        [Authorize(Roles ="SystemAdmin")]
        [ProducesResponseType(201, Type = typeof(ResAdminAddingDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult AddAdmin(ReqAdminAddingDto adminData)
        {
            if (adminData == null)
                return BadRequest(ModelState);
            var newAdmin = _unitOfWork._customerRepository.GetUsers().Where(d => d.Phone == adminData.Phone).FirstOrDefault();
            if(newAdmin != null)
            {
                ModelState.AddModelError("", "User allready exists");
                return StatusCode(422, ModelState);
            }
            var adminMap = _mapper.Map<User>(adminData);
            adminMap.Role = "Admin";
            adminMap.Password = "123456789";
            adminMap.CreatedAt = DateTime.Now;
            adminMap.UpdatedAt = DateTime.Now;
            adminMap.IsNotDeleted = true;
            if(!_unitOfWork._systemAdminRepository.AddAdmin(adminMap))
            {
                ModelState.AddModelError("", "Somthing went wrong while saving");
                return StatusCode(500, ModelState);
            }
            var center = (_unitOfWork._systemAdminRepository.GetCenters().Where(c => c.Id == adminData.CenterId).FirstOrDefault());
            var statusId = (_unitOfWork._systemAdminRepository.GetStatuses().Where(s => s.Name == "نشط").FirstOrDefault()).Id;
            _unitOfWork._systemAdminRepository.AddAdmin(new Admin { CenterId = center.Id, UserId = adminMap.Id, StatusId =  statusId });
            var resAdmin = new ResAdminAddingDto
            {
                Name = adminMap.Name,
                Phone = adminMap.Phone,
                Email = adminMap.Email,
                Password = adminMap.Password,
                CenterName = center.Name
            };
            
            return Ok("Successfully added");

        }

        [HttpPost("add-center")]
        [Authorize(Roles = "SystemAdmin")]
        [ProducesResponseType(201)]
        public IActionResult AddCenter(ReqCenterAddingDto centerData)
        {
            if (centerData == null)
                return BadRequest(ModelState);
            var newCenter = _unitOfWork._systemAdminRepository.GetCenters().
                Where(c => c.Name == centerData.Name && c.NeighborhoodId == centerData.NeighborhoodId).FirstOrDefault();
            if(newCenter != null)
            {
                ModelState.AddModelError("", "Center Already Exists");
                return StatusCode(422, ModelState);
            }
            var centerMap = _mapper.Map<Center>(centerData);
            if(!_unitOfWork._systemAdminRepository.AddCenter(centerMap))
            {
                ModelState.AddModelError("", "Somthing went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully added");
        }

        [HttpGet("get-all-orders")]
        [ProducesResponseType(200)]
        [Authorize(Roles = "SystemAdmin")]
        public IActionResult GetOrders([FromQuery] int statusId)
        {
            
            if(statusId == null || statusId == 0)
            {
                var orders = _unitOfWork._systemAdminRepository.GetOrders();
                var resOrders = _mapper.Map<List<ResOrderDto>>(orders);
                return Ok(resOrders);
            }
            else
            {
                var orders = _unitOfWork._systemAdminRepository.GetOrdersByStatus(statusId);
                var resOrders = _mapper.Map<List<ResOrderDto>>(orders);
                return Ok(resOrders);
            }
        }

        [HttpGet("get-centers")]
        [ProducesResponseType(200)]
        [Authorize(Roles = "SystemAdmin")]
        public IActionResult GetCenters()
        {
            var centers = _unitOfWork._systemAdminRepository.GetCenters();
            var resCenters = _mapper.Map<List<ResCentersDto>>(centers);
            foreach (var center in resCenters)
            {
                var neighborhoodId = _unitOfWork._systemAdminRepository.GetNeighborhoodIdByCenterId(center.Id);
                var neighborhoodName = _unitOfWork._orderRepository.GetNeighborhoodName(neighborhoodId);
                center.NeighborhoodName = neighborhoodName;
            }
            return Ok(resCenters);
        }

        [HttpGet("get-admins-by-cneter")]
        [ProducesResponseType(200)]
        [Authorize(Roles = "SystemAdmin")]
        public IActionResult GetAdminsByCenter(int centerId)
        {
            var admins = _unitOfWork._systemAdminRepository.GetAdminsByCenterId(centerId);
            var resAdmins = _mapper.Map<List<ResAdminDto>>(admins);
            return Ok(resAdmins);
        }
    }
}
