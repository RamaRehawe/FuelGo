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
        public SystemAdminController(UserInfoService userInfoService, IUserRepository userRepository, IMapper mapper) : 
            base(userInfoService, userRepository)
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
            var newAdmin = _UOF._customerRepository.GetUsers().Where(d => d.Phone == adminData.Phone).FirstOrDefault();
            if(newAdmin != null)
            {
                ModelState.AddModelError("", "Admin allready exists");
                return StatusCode(422, ModelState);
            }
            var adminMap = _mapper.Map<User>(adminData);
            adminMap.Role = "Admin";
            adminMap.Password = "123456789";
            adminMap.CreatedAt = DateTime.Now;
            if(!_UOF._systemAdminRepository.AddAdmin(adminMap))
            {
                ModelState.AddModelError("", "Somthing went wrong while saving");
                return StatusCode(500, ModelState);
            }
            var admin = _UOF._systemAdminRepository.GetAdminById(adminMap.Id);
            admin.CenterId = adminData.CenterId;
            var center = _UOF._adminRepository.GetCenterByAdminId(adminMap.Id);
            var resAdmin = new ResAdminAddingDto
            {
                Name = adminMap.Name,
                Phone = adminMap.Phone,
                Email = adminMap.Email,
                Password = adminMap.Password,
                CenterName = center.Name
            };
            if (!_UOF.Save())
            {
                ModelState.AddModelError("", "Somthing went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully added");

        }

        [HttpPost("add-center")]
        [Authorize(Roles = "SystemAdmin")]
        [ProducesResponseType(201)]
        public IActionResult AddCenter(ReqCenterAddingDto centerData)
        {
            if (centerData == null)
                return BadRequest(ModelState);
            var newCenter = _UOF._systemAdminRepository.GetCenters().
                Where(c => c.Name == centerData.Name && c.NeighborhoodId == centerData.NeighborhoodId).FirstOrDefault();
            if(newCenter != null)
            {
                ModelState.AddModelError("", "Center Already Exists");
                return StatusCode(422, ModelState);
            }
            var centerMap = _mapper.Map<Center>(centerData);
            if(!_UOF._systemAdminRepository.AddCenter(centerMap))
            {
                ModelState.AddModelError("", "Somthing went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully added");
        }

    }
}
