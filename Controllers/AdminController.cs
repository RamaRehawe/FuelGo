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
    public class AdminController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IAdminRepository _adminRepository;
        public AdminController(UserInfoService userInfoService, IUserRepository userRepository, 
            IMapper mapper, IAdminRepository adminRepository) : 
            base(userInfoService, userRepository)
        {
            _mapper = mapper;
            _adminRepository = adminRepository;
        }

        [HttpPost("add-driver")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(201, Type = typeof(ResDriverAddingDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult AddDriver(ReqDriverAddingDto driverData)
        {
            if (driverData == null)
                return BadRequest(ModelState);
            var newDriver = _UOF._customerRepository.GetUsers().Where(d => d.Phone == driverData.Phone).FirstOrDefault();
            if(newDriver != null)
            {
                ModelState.AddModelError("", "Driver allready exists");
                return StatusCode(422, ModelState);
            }
            var driverMap = _mapper.Map<User>(driverData);
            driverMap.Role = "Driver";
            driverMap.Password = "123456789";
            driverMap.CreatedAt = DateTime.Now;
            var adminId = base.GetActiveUser().Id;
            var center = _adminRepository.GetCenterByAdminId(adminId);
            var shiftName = (_adminRepository.GetShifts().Where(s => s.Id == driverData.ShiftId).FirstOrDefault()).ShiftName;
            var truckPlateNum = (_adminRepository.GetTrucks().Where(t => t.Id == driverData.TruckId).FirstOrDefault()).PlateNumber;
            if(!_adminRepository.AddDriver(driverMap))
            {
                ModelState.AddModelError("", "Somthing went wrong while saving");
                return StatusCode(500, ModelState);
            }
            var driver = _adminRepository.GetDriverByUserId(driverMap.Id);
            driver.CenterId = center.Id;
            driver.ShiftId = driverData.ShiftId;
            driver.TruckId = driverData.TruckId;
            var resdriver = new ResDriverAddingDto
            {
                Name = driverData.Name,
                Phone = driverData.Phone,
                Email = driverData.Email,
                Password = driverMap.Password,
                ShiftName = shiftName,
                TruckPlateNumber = truckPlateNum,
                CenterName = center.Name
            };
            if(!_UOF.Save())
            {
                ModelState.AddModelError("", "Somthing went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully added");

        }
            
    }
}
