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
        private readonly ICustomerRepository _customerRepository;
        private readonly ISystemAdminRepository _systemAdminRepository;
        private readonly IAdminRepository _adminRepository;

        public SystemAdminController(UserInfoService userInfoService, IUserRepository userRepository, IMapper mapper,
            ICustomerRepository customerRepository, ISystemAdminRepository systemAdminRepository, IAdminRepository adminRepository) : 
            base(userInfoService, userRepository)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
            _systemAdminRepository = systemAdminRepository;
            _adminRepository = adminRepository;
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
            var newAdmin = _customerRepository.GetUsers().Where(d => d.Phone == adminData.Phone).FirstOrDefault();
            if(newAdmin != null)
            {
                ModelState.AddModelError("", "Admin allready exists");
                return StatusCode(422, ModelState);
            }
            var adminMap = _mapper.Map<User>(adminData);
            adminMap.Role = "Admin";
            adminMap.Password = "123456789";
            adminMap.CreatedAt = DateTime.Now;
            adminMap.UpdatedAt = DateTime.Now;
            if(!_systemAdminRepository.AddAdmin(adminMap))
            {
                ModelState.AddModelError("", "Somthing went wrong while saving");
                return StatusCode(500, ModelState);
            }
            var center = (_systemAdminRepository.GetCenters().Where(c => c.Id == adminData.CenterId).FirstOrDefault());
            var statusId = (_systemAdminRepository.GetStatuses().Where(s => s.Name == "نشط").FirstOrDefault()).Id;
            _systemAdminRepository.AddAdmin(new Admin { CenterId = center.Id, UserId = adminMap.Id, StatusId =  statusId });
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
            var newCenter = _systemAdminRepository.GetCenters().
                Where(c => c.Name == centerData.Name && c.NeighborhoodId == centerData.NeighborhoodId).FirstOrDefault();
            if(newCenter != null)
            {
                ModelState.AddModelError("", "Center Already Exists");
                return StatusCode(422, ModelState);
            }
            var centerMap = _mapper.Map<Center>(centerData);
            if(!_systemAdminRepository.AddCenter(centerMap))
            {
                ModelState.AddModelError("", "Somthing went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully added");
        }
    }
}
