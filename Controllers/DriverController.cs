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
    public class DriverController : BaseController
    {
        private readonly IMapper _mapper;

        public DriverController(UserInfoService userInfoService, IUnitOfWork unitOfWork, IMapper mapper) : 
            base(userInfoService, unitOfWork)
        {
            _mapper = mapper;
        }

        [HttpGet("get-pending-orders")]
        [Authorize(Roles = "Driver")]
        [ProducesResponseType(200)]
        public IActionResult GetPendingOrders()
        {
            var statusId = _unitOfWork._orderRepository.GetStatuses().Where(s => s.Name == "قيد الانتظار").FirstOrDefault().Id;
            var truck = _unitOfWork._orderRepository.GetTruck(
                _unitOfWork._orderRepository.GetDriver(base.GetActiveUser()!.Id).TruckId);
            var orders = _unitOfWork._driverRepository.GetPendingOrders(statusId).Where(o => o.FuelTypeId == truck.CargoTankTypeId);
            var resOrders = _mapper.Map<List<ResPendingOrdersDto>>(orders);
            return Ok(resOrders);
        }

        [HttpPost("start-job")]
        [Authorize(Roles = "Driver")]
        [ProducesResponseType(200)]
        public IActionResult StartJob()
        {
            var userId = base.GetActiveUser()!.Id;
            var driver = _unitOfWork._orderRepository.GetDriver(userId);
            var statusId = _unitOfWork._orderRepository.GetStatuses().Where(s => s.Name == "متاح").FirstOrDefault().Id;
            driver.StatusId = statusId;
            driver.IsDriving = true;
            _unitOfWork.Commit();
            return Ok("Started");
        }

        [HttpPost("EndJob")]
        [Authorize(Roles = "Driver")]
        public IActionResult EndJob()
        {
            var userId = base.GetActiveUser()!.Id;
            var driver = _unitOfWork._orderRepository.GetDriver(userId);
            var statusId = _unitOfWork._orderRepository.GetStatuses().Where(s => s.Name == "غير متصل").FirstOrDefault().Id;
            driver.StatusId = statusId;
            driver.IsDriving = false;
            _unitOfWork.Commit();
            return Ok("Driver Ended Job");
        }

        [HttpPost("accept-order")]
        [Authorize(Roles = "Driver")]
        [ProducesResponseType(200)]
        public IActionResult AcceptOrder(ReqAcceptOrderDto orderData)
        {
            if (orderData == null)
                return BadRequest(ModelState);
            var order = _unitOfWork._orderRepository.GetOrder(orderData.OrderNumber);
            if (order == null)
                return NotFound("Order not found.");
            var statusId = _unitOfWork._orderRepository.GetStatuses().Where(s => s.Name == "في الطريق").FirstOrDefault().Id;
            
            var driverId = _unitOfWork._orderRepository.GetDriverId(base.GetActiveUser()!.Id);
            var driver = _unitOfWork._orderRepository.GetDriver(base.GetActiveUser()!.Id);
            var truck = _unitOfWork._orderRepository.GetTruck(driver.TruckId);
            var fuelPrice = _unitOfWork._orderRepository.GetFuelPrice(order.FuelTypeId);
            order.Price = (fuelPrice * order.OrderedQuantity) +
                CalculateDeliveryPrice(order.CustomerLat, order.CustomerLong, truck.Lat, truck.Long,
                order.OrderedQuantity);
            order.StatusId = statusId;
            order.DriverId = driverId;
            order.DriverLat = truck.Lat;
            order.DriverLong = truck.Long;
            
            order.IsActive = true;
            if (!_unitOfWork._orderRepository.UpdateOrder(order))
            {
                ModelState.AddModelError("", "Somthing went wrong while saving");
                return StatusCode(500, ModelState);
            }
            var resOrder = _mapper.Map<ResPendingOrdersDto>(order);
            return Ok(resOrder);
        }

        [HttpGet("get-active-order")]
        [Authorize(Roles = "Driver")]
        [ProducesResponseType(200)]
        public IActionResult GetActiveOrder()
        {
            var userId = base.GetActiveUser()!.Id;
            var driver = _unitOfWork._orderRepository.GetDriver(userId);
            var order = _unitOfWork._driverRepository.GetActiveOrderByDriverId(driver.Id);
            var resOrder = _mapper.Map<ResPendingOrdersDto>(order);
            return Ok(resOrder);
        }

        [HttpPost("arrived-location")]
        [Authorize(Roles = "Driver")]
        [ProducesResponseType(200)]
        public IActionResult ArrivedLocation()
        {
            var userId = base.GetActiveUser()!.Id;
            var driver = _unitOfWork._orderRepository.GetDriver(userId);
            var order = _unitOfWork._driverRepository.GetActiveOrderByDriverId(driver.Id);
            var statusId = _unitOfWork._orderRepository.GetStatuses().Where(s => s.Name == "وصل للموقع").FirstOrDefault().Id;
            order.StatusId = statusId;
            _unitOfWork.Commit();
            return Ok("driver arrived location");
        }

        [HttpPost("start-servicing-order")]
        [Authorize(Roles = "Driver")]
        [ProducesResponseType(200)]
        public IActionResult StartServiceing(string authCode)
        {
            var userId = base.GetActiveUser()!.Id;
            var driver = _unitOfWork._orderRepository.GetDriver(userId);
            var order = _unitOfWork._driverRepository.GetActiveOrderByDriverId(driver.Id);
            var statusId = _unitOfWork._orderRepository.GetStatuses().Where(s => s.Name == "بدء تعبئة الطلب").FirstOrDefault().Id;
            if(order.AuthCode != authCode)
            {
                ModelState.AddModelError("", "the code is wrong");
                return StatusCode(400, ModelState);
            }
            order.StatusId = statusId;
            _unitOfWork.Commit();
            return Ok("driver is servicing the order");
        }

        [HttpPost("complete-order")]
        [Authorize(Roles = "Driver")]
        [ProducesResponseType(200)]
        public IActionResult CompleteOrder(double quantity)
        {
            var userId = base.GetActiveUser()!.Id;
            var driver = _unitOfWork._orderRepository.GetDriver(userId);
            var order = _unitOfWork._driverRepository.GetActiveOrderByDriverId(driver.Id);
            var statusId = _unitOfWork._orderRepository.GetStatuses().Where(s => s.Name == "تم التسليم").FirstOrDefault().Id;
            order.FinalQuantity = quantity;
            order.StatusId = statusId;
            var fuelPrice = _unitOfWork._orderRepository.GetFuelPrice(order.FuelTypeId);
            order.FinalPrice = (fuelPrice * quantity) +
                CalculateDeliveryPrice(order.CustomerLat, order.CustomerLong, order.DriverLat, order.DriverLong, 
                quantity);
            var freeDelivery = GetConstantValue("حد التوصيل المجاني");
            if (order.OrderedQuantity >= freeDelivery && quantity < freeDelivery)
            {
                order.FinalPrice += 10000;
            }
            _unitOfWork._driverRepository.UpdateOrder(order);
            
            return Ok("Order Completeed");
        }

        [HttpGet("get-my-orders")]
        [Authorize(Roles = "Driver")]
        [ProducesResponseType(200)]
        public IActionResult GetMyOrders()
        {
            var driverId = _unitOfWork._orderRepository.GetDriverId(base.GetActiveUser()!.Id);
            var orders = _unitOfWork._driverRepository.GetOrders(driverId);
            var resOrders = _mapper.Map<List<ResOrderDto>>(orders);
            return Ok(resOrders);
        }

        [HttpGet("cargo-tank")]
        [Authorize(Roles = "Driver")]
        [ProducesResponseType(200)]
        public IActionResult GetMyCargoTank()
        {
            var driver = _unitOfWork._orderRepository.GetDriver(base.GetActiveUser()!.Id);
            var truck = _unitOfWork._orderRepository.GetTruck(driver.TruckId);
            return Ok(truck.CargoTankCapacity);
        }
    }
}
