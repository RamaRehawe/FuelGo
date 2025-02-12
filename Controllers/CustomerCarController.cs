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
    public class CustomerCarController : BaseController
    {
        private readonly IMapper _mapper;
        public CustomerCarController(UserInfoService userInfoService, IUnitOfWork unitOfWork, IMapper mapper) : 
            base(userInfoService, unitOfWork)
        {
            _mapper = mapper;
        }

        [HttpPost("add-car")]
        [ProducesResponseType(201, Type = typeof(AddCarDto))]
        [Authorize(Roles = "Customer")]
        public IActionResult AddCar(AddCarDto carData)
        {
            if (carData == null)
                return BadRequest(ModelState);
            var car = _unitOfWork._customerCarRepository.GetCars().Where(c => c.PlateNumber == carData.PlateNumber).FirstOrDefault();
            if(car != null)
            {
                ModelState.AddModelError("", "Car already exists");
                return StatusCode(422, ModelState);
            }
            var carMap = _mapper.Map<CustomerCar>(carData);
            carMap.CustomerId = _unitOfWork._orderRepository.GetCustomerId(base.GetActiveUser()!.Id);
            carMap.IsDeleted = false;
            if(!_unitOfWork._customerCarRepository.AddCar(carMap))
            {
                ModelState.AddModelError("", "Somthing went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok(carData);
        }

        [HttpPatch("delete-car")]
        public IActionResult DeleteCar(string plateNum)
        {
            if(plateNum == null)
            {
                return BadRequest(ModelState);
            }
            var car = _unitOfWork._customerCarRepository.GetCars().Where(cc => cc.PlateNumber == plateNum).FirstOrDefault();
            if(car == null)
            {
                ModelState.AddModelError("", "Car not found");
                return StatusCode(404, ModelState);
            }
            car.IsDeleted = true;
            _unitOfWork.Commit();
            return Ok("Car Deleted Successfully");
        }
    }
}
