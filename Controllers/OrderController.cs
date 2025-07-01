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
            var center = _unitOfWork._orderRepository.GetCenterByCityId(orderData.CityId);
            var fuelPrice = _unitOfWork._orderRepository.GetFuelPrice(orderMap.FuelTypeId, center.Id);
            var fee = CalculateDeliveryPrice(orderMap.CustomerLat, orderMap.CustomerLong, center.Lat, center.Long,
                orderMap.OrderedQuantity);
            orderMap.Price = (fuelPrice * orderMap.OrderedQuantity) + fee;

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
                StatusName = "قيد الانتظار",
                StatusId = orderMap.StatusId,
                TotalPrice = orderMap.Price,
                Fee = fee
            };
            return Ok(resOrder);
        }

        [HttpPost("place-house-order")]
        [Authorize(Roles = "Customer")]
        [ProducesResponseType(201, Type = typeof(ResPlaceHouseOrderDto))]
        [ProducesResponseType(500)]
        public IActionResult PlaceHouseOrder(ReqPlaceHouseOrderDto orderData)
        {
            if (orderData == null)
                return BadRequest(ModelState);
            var orderMap = _mapper.Map<Order>(orderData);
            orderMap.Date = DateTime.Now;
            orderMap.OrderNumber = GenerateRandomCode(6);
            var apt = _unitOfWork._orderRepository.GetApartmentById(orderData.CustomerApartmentId);
            orderMap.CustomerLat = apt.Lat;
            orderMap.CustomerLong = apt.Long;
            orderMap.LocationDescription = apt.LocationDescription;
            orderMap.NeighborhoodId = apt.NeighborhoodId;
            orderMap.IsItUrgent = false;
            orderMap.CustomerId = _unitOfWork._orderRepository.GetCustomerId(base.GetActiveUser()!.Id);
            var statusId = _unitOfWork._orderRepository.GetStatuses().Where(s => s.Name == "قيد الانتظار").FirstOrDefault().Id;
            orderMap.StatusId = statusId;
            orderMap.IsActive = true;
            orderMap.AuthCode = GenerateRandomCode(10);

            var city = _unitOfWork._orderRepository.GetCityByNeighborhood(apt.NeighborhoodId);
            var center = _unitOfWork._orderRepository.GetCenterByCityId(city.Id);
            var fuelPrice = _unitOfWork._orderRepository.GetFuelPrice(orderMap.FuelTypeId, center.Id);
            var fee = CalculateDeliveryPrice(orderMap.CustomerLat, orderMap.CustomerLong, center.Lat, center.Long,
                orderMap.OrderedQuantity); ;
            orderMap.Price = (fuelPrice * orderMap.OrderedQuantity) + fee;
            if (!_unitOfWork._orderRepository.AddOrder(orderMap))
            {
                ModelState.AddModelError("", "Somthing went wrong while saving");
                return StatusCode(500, ModelState);
            }
            var neighborhoodName = _unitOfWork._orderRepository.GetNeighborhoodName(orderMap.NeighborhoodId);
            var cityName = _unitOfWork._orderRepository.GetCityName(orderMap.NeighborhoodId);
            var fuelName = _unitOfWork._orderRepository.GetFuelName(orderMap.FuelTypeId);
            var resOrder = new ResPlaceHouseOrderDto
            {
                Date = orderMap.Date,
                OrderNumber = orderMap.OrderNumber,
                LocationDescription = orderMap.LocationDescription,
                NeighborhoodName = neighborhoodName,
                CityName = cityName,
                FuelTypeName = fuelName,
                OrderedQuantity = orderMap.OrderedQuantity,
                StatusName = "قيد الانتظار",
                StatusId = orderMap.StatusId,
                Apartment = apt,
                TotalPrice = orderMap.Price,
                Fee = fee
            };
            return Ok(resOrder);
        }

        [HttpGet("track-order")]
        [Authorize(Roles = "Customer")]
        [ProducesResponseType(200)]
        public IActionResult TrackOrder()
        {
            var userId = base.GetActiveUser()!.Id;
            var customerId = _unitOfWork._orderRepository.GetCustomerId(userId);
            var order = _unitOfWork._orderRepository.GetActiveOrderByCustomerId(customerId);
            if (order == null)
                return NotFound("No active order found.");
            var status = _unitOfWork._orderRepository.GetStatuses()
                  .FirstOrDefault(o => o.Id == order.StatusId)?.Name;
            var statusId = _unitOfWork._orderRepository.GetStatuses()
                  .FirstOrDefault(o => o.Id == order.StatusId)?.Id;
            TimeSpan? estimatedTime = CalculateEstimatedTime(order);
            return Ok(new
            {
                Status = status,
                StatusId = statusId,
                EstimatedDeliveryTime = estimatedTime?.ToString(@"hh\:mm\:ss") ?? "Calculating..."
            });
        }

        [HttpGet("get-authcode")]
        [Authorize(Roles = "Customer")]
        [ProducesResponseType(200)]
        public IActionResult GetAuthCode()
        {
            var userId = base.GetActiveUser()!.Id;
            var customerId = _unitOfWork._orderRepository.GetCustomerId(userId);
            var order = _unitOfWork._orderRepository.GetActiveOrderByCustomerId(customerId);
            if(order == null)
            {
                return NotFound("No active order found.");
            }
            if (order.DriverId == null)
                return NotFound("No driver yet.");
            var driver = _unitOfWork._customerRepository.GetdriverById((int)order.DriverId);
            var driverUser = _unitOfWork._customerRepository.GetUsers().Where(u => u.Id == driver.UserId).FirstOrDefault();
            return Ok(new
            {
                AuthCode = order.AuthCode,
                DriverName = driverUser.Name,
                DriverPhone = driverUser.Phone
            });
        }

        [HttpGet("get-active-order-status")]
        [Authorize(Roles = "Driver")]
        [ProducesResponseType(200)]
        public IActionResult GetActiveOrderStatus()
        {
            var userId = base.GetActiveUser()!.Id;
            var driver = _unitOfWork._orderRepository.GetDriver(userId);
            var order = _unitOfWork._orderRepository.GetActiveOrderByDriverId(driver.Id);
            if (order == null)
                return NotFound("No active order found.");
            var status = _unitOfWork._orderRepository.GetStatuses()
                  .FirstOrDefault(o => o.Id == order.StatusId);
            var resStatus = _mapper.Map<ResStatusDto>(status);
            return Ok(resStatus);
        }

    }
}
