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

        public AdminController(UserInfoService userInfoService, IUnitOfWork unitOfWork, IMapper mapper) : 
            base(userInfoService, unitOfWork)
        {
            _mapper = mapper;
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
            var newDriver = _unitOfWork._customerRepository.GetUsers().Where(d => d.Phone == driverData.Phone).FirstOrDefault();
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
            var adminId = _unitOfWork._adminRepository.GetAdminByUserId(base.GetActiveUser()!.Id).Id;
            var center = _unitOfWork._adminRepository.GetCenterByAdminId(adminId);
            var shift = (_unitOfWork._adminRepository.GetShifts().Where(s => s.Id == driverData.ShiftId).FirstOrDefault());
            var truck = (_unitOfWork._adminRepository.GetTrucks().Where(t => t.Id == driverData.TruckId).FirstOrDefault());
            if(!_unitOfWork._adminRepository.AddDriver(driverMap))
            {
                ModelState.AddModelError("", "Somthing went wrong while saving");
                return StatusCode(500, ModelState);
            }
            var status = _unitOfWork._adminRepository.GetStatuses().Where(s => s.Name == "غير متصل").FirstOrDefault();
            _unitOfWork._adminRepository.AddDriver(new Driver { 
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
            var newTruck = _unitOfWork._adminRepository.GetTrucks().
                Where(t => t.PlateNumber == truckData.PlateNumber).FirstOrDefault();
            if(newTruck != null)
            {
                ModelState.AddModelError("", "Truck allready exists");
                return StatusCode(422, ModelState);
            }
            var truckMap = _mapper.Map<Truck>(truckData);
            var adminId = _unitOfWork._adminRepository.GetAdminByUserId(base.GetActiveUser()!.Id).Id;
            var center = _unitOfWork._adminRepository.GetCenterByAdminId(adminId);
            truckMap.CenterId = center.Id;
            if(!_unitOfWork._adminRepository.AddTruck(truckMap))
            {
                ModelState.AddModelError("", "Somthing went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Added Successfully");
        }

        [HttpPost("edit-fuel-price")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(200)]
        public IActionResult EditFuelPrice(ReqEditFuelPriceDto prices)
        {
            if (prices == null)
                return BadRequest(ModelState);
            var admin = _unitOfWork._adminRepository.GetAdminByUserId(base.GetActiveUser()!.Id);
            var fuel = _unitOfWork._adminRepository.GetFuelByCenterAndFuelId(admin.CenterId, prices.FuelTypeId);
            fuel.Price = prices.Price;
            _unitOfWork.Commit();
            return Ok("edited succesfully");
        }

        [HttpGet("get-orders-by-center")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(200)]
        public IActionResult GetOrdersByCenter()
        {
            var admin = _unitOfWork._adminRepository.GetAdminByUserId(base.GetActiveUser()!.Id);
            var orders = _unitOfWork._adminRepository.GetOrdersByCenterId(admin.CenterId);
            var resOrders = _mapper.Map<List<ResOrderDto>>(orders);
            return Ok(resOrders);
        }

        [HttpGet("get-drivers")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(200)]
        public IActionResult GetDrivers()
        {
            var admin = _unitOfWork._adminRepository.GetAdminByUserId(base.GetActiveUser()!.Id);
            var drivers = _unitOfWork._adminRepository.GetDriversByCenter(admin.CenterId);
            var resDrivers = _mapper.Map<List<ResDriversDto>>(drivers);
            return Ok(resDrivers);
        }
    }
}
