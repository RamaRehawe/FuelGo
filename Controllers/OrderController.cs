using AutoMapper;
using FuelGo.Dto;
using FuelGo.Inerfaces;
using FuelGo.Models;
using FuelGo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FuelGo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : BaseController
    {
        private readonly IMapper _mapper;
        

        public OrderController(UserInfoService userInfoService, IUnitOfWork unitOfWork, IMapper mapper) : 
            base(userInfoService, unitOfWork)
        {
            _mapper = mapper;
        }

        [HttpPost("place-car-order")]
        [Authorize(Roles = "Customer")]
        [ProducesResponseType(201, Type = typeof(ResPlaceCarOrderDto))]
        [ProducesResponseType(500)]
        public IActionResult PlaceCarOrder(ReqPlaceCarOrderDto orderData)
        {
            if (orderData == null)
                return BadRequest(ModelState);
            var orderMap = _mapper.Map<Order>(orderData);
            orderMap.CustomerLat = orderData.CustomerLat;
            orderMap.CustomerLong = orderData.CustomerLong;
            orderMap.Date = DateTime.Now;
            orderMap.OrderNumber = GenerateRandomCode(6);
            orderMap.IsItUrgent = false;
            orderMap.CustomerId = _unitOfWork._orderRepository.GetCustomerId( base.GetActiveUser()!.Id);
            var statusId = _unitOfWork._orderRepository.GetStatuses().Where(s => s.Name == "قيد الانتظار").FirstOrDefault().Id;
            orderMap.StatusId = statusId;
            orderMap.IsActive = true;
            orderMap.AuthCode = GenerateRandomCode(10);
            
            if (!_unitOfWork._orderRepository.AddOrder(orderMap))
            {
                ModelState.AddModelError("", "Somthing went wrong while saving");
                return StatusCode(500, ModelState);
            }
            
            var neighborhoodName = _unitOfWork._orderRepository.GetNeighborhoodName(orderMap.NeighborhoodId);
            var cityName = _unitOfWork._orderRepository.GetCityName(orderMap.NeighborhoodId);
            var fuelName = _unitOfWork._orderRepository.GetFuelName(orderMap.FuelTypeId);
            var carBrand = _unitOfWork._orderRepository.GetCarBrand(orderMap.CustomerCarId);
            var resOrder = new ResPlaceCarOrderDto
            {
                Date = orderMap.Date,
                OrderNumber = orderMap.OrderNumber,
                LocationDescription = orderMap.LocationDescription,
                NeighborhoodName = neighborhoodName,
                CityName = cityName,
                FuelTypeName = fuelName,
                OrderedQuantity = orderMap.OrderedQuantity,
                CustomerCarBrand = carBrand,
                StatusName = "قيد الانتظار"
            };
            return Ok(resOrder);
        }

        [HttpGet("track-order")]
        [ProducesResponseType(200)]
        public IActionResult TrackOrder()
        {
            var userId = base.GetActiveUser()!.Id;
            var customerId = _unitOfWork._orderRepository.GetCustomerId(userId);
            var order = _unitOfWork._orderRepository.GetActiveOrderByCustomerId(customerId);

            return Ok(_unitOfWork._orderRepository.GetStatuses().Where(o => o.Id == order.StatusId).FirstOrDefault().Name);
        }
        

    }
}
