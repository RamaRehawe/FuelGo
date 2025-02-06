using AutoMapper;
using FuelGo.Dto;
using FuelGo.Inerfaces;
using FuelGo.Models;
using FuelGo.Repository;
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
        private readonly ISystemAdminRepository _systemAdminRepository;
        private readonly ICustomerRepository _customerRepository;

        public AdminController(UserInfoService userInfoService, IUserRepository userRepository, IMapper mapper,
            IAdminRepository adminRepository, ICustomerRepository customerRepository) : 
            base(userInfoService, userRepository)
        {
            _mapper = mapper;
            _adminRepository = adminRepository;
            _customerRepository = customerRepository;
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
            var newDriver = _customerRepository.GetUsers().Where(d => d.Phone == driverData.Phone).FirstOrDefault();
            if(newDriver != null)
            {
                ModelState.AddModelError("", "Driver allready exists");
                return StatusCode(422, ModelState);
            }
            var driverMap = _mapper.Map<User>(driverData);
            driverMap.Role = "Driver";
            driverMap.Password = "123456789";
            driverMap.CreatedAt = DateTime.Now;
            driverMap.UpdatedAt = DateTime.Now;
            var adminId = base.GetActiveUser().Id;
            var center = _adminRepository.GetCenterByAdminId(adminId);
            var shift = (_adminRepository.GetShifts().Where(s => s.Id == driverData.ShiftId).FirstOrDefault());
            var truck = (_adminRepository.GetTrucks().Where(t => t.Id == driverData.TruckId).FirstOrDefault());
            if(!_adminRepository.AddDriver(driverMap))
            {
                ModelState.AddModelError("", "Somthing went wrong while saving");
                return StatusCode(500, ModelState);
            }
            var status = (_adminRepository.GetStatuses().Where(s => s.Name == "مشغول").FirstOrDefault());
            _adminRepository.AddDriver(new Driver { 
                UserId = driverMap.Id, ShiftId = shift.Id, StatusId = status.Id, 
                TruckId = truck.Id, CenterId = center.Id, IsDriving = false });
            
            var resdriver = new ResDriverAddingDto
            {
                Name = driverData.Name,
                Phone = driverData.Phone,
                Email = driverData.Email,
                Password = driverMap.Password,
                ShiftName = shift.ShiftName,
                TruckPlateNumber = truck.PlateNumber,
                CenterName = center.Name
            };
            
            return Ok("Successfully added");

        }

        [HttpPost("add-truck")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(201)]
        public IActionResult AddTruck(ReqTruckAddingDto truckData)
        {
            if (truckData == null)
                return BadRequest(ModelState);
            var newTruck = _adminRepository.GetTrucks().
                Where(t => t.PlateNumber == truckData.PlateNumber).FirstOrDefault();
            if(newTruck != null)
            {
                ModelState.AddModelError("", "Truck allready exists");
                return StatusCode(422, ModelState);
            }
            var truckMap = _mapper.Map<Truck>(truckData);
            var adminId = base.GetActiveUser().Id;
            var center = _adminRepository.GetCenterByAdminId(adminId);
            truckMap.CenterId = center.Id;
            if(!_adminRepository.AddTruck(truckMap))
            {
                ModelState.AddModelError("", "Somthing went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Added Successfully");
        }
    }
}
