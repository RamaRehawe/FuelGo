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
    public class TankRefillController : BaseController
    {
        private readonly IMapper _mapper;
        public TankRefillController(UserInfoService userInfoService, IUnitOfWork unitOfWork, IMapper mapper) : 
            base(userInfoService, unitOfWork)
        {
            _mapper = mapper;
        }

        [HttpPost("Refill")]
        [Authorize(Roles = "Driver")]
        public IActionResult RefillTank(ReqRefillDto refillData)
        {
            if (refillData == null)
                return BadRequest(ModelState);
            var refillMap = _mapper.Map<TruckTankRefill>(refillData);
            var driver = _unitOfWork._orderRepository.GetDriver(base.GetActiveUser()!.Id);
            refillMap.DriverId = driver.Id;
            refillMap.TruckId = (int)driver.TruckId;
            var truck = _unitOfWork._orderRepository.GetTruck(driver.TruckId);
            truck.CargoTankCapacity += refillMap.QuantityCargoRefill;
            truck.FuelTankCapacity += refillMap.QuantityFuelRefill;
            if(!_unitOfWork._tankRefillRepository.Refill(refillMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("tank refilled");
        }
    }
}
