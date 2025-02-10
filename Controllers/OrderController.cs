using AutoMapper;
using FuelGo.Dto;
using FuelGo.Inerfaces;
using FuelGo.Models;
using FuelGo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FuelGo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : BaseController
    {
        private readonly IMapper _mapper;
        private static Random random = new Random();
        private static HashSet<string> orderNumbers = new HashSet<string>();

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
            orderMap.Date = DateTime.Now;
            orderMap.OrderNumber = GenerateRandomCode(6);
            orderMap.IsItUrgent = false;
            orderMap.CustomerId = _unitOfWork._orderRepository.GetCustomerId( base.GetActiveUser()!.Id);
            var statusId = _unitOfWork._orderRepository.GetStatuses().Where(s => s.Name == "قيد الانتظار").FirstOrDefault().Id;
            orderMap.StatusId = statusId;
            orderMap.IsActive = true;
            orderMap.AuthCode = GenerateRandomCode(10);
            if(!_unitOfWork._orderRepository.AddOrder(orderMap))
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

        [HttpGet("get-pending-orders")]
        [Authorize(Roles = "Driver")]
        [ProducesResponseType(200)]
        public IActionResult GetPendingOrders()
        {
            var statusId = _unitOfWork._orderRepository.GetStatuses().Where(s => s.Name == "قيد الانتظار").FirstOrDefault().Id;
            var orders = _unitOfWork._orderRepository.GetPendingOrders(statusId);
            return Ok(orders);

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
            var fuelPrice = _unitOfWork._orderRepository.GetFuelPrice(order.FuelTypeId);
            var driverId = _unitOfWork._orderRepository.GetDriverId(base.GetActiveUser()!.Id);
            var driver = _unitOfWork._orderRepository.GetDriver(base.GetActiveUser()!.Id);
            var truck = _unitOfWork._orderRepository.GetTruck(driver.TruckId);
            order.StatusId = statusId;
            order.DriverId = driverId;
            order.FinalPrice = (fuelPrice * order.OrderedQuantity) + 
                CalculateDeliveryPrice(order.Lat, order.Long, truck.Lat, truck.Long, order.OrderedQuantity);
            order.IsActive = true;
            if(!_unitOfWork._orderRepository.UpdateOrder(order))
            {
                ModelState.AddModelError("", "Somthing went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok(order);
        }
        private string GenerateRandomCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string orderNum;
            do
            {
                StringBuilder resulte = new StringBuilder(length);
                for (int i = 0; i < 6; i++)
                {
                    resulte.Append(chars[random.Next(chars.Length)]);
                }
                orderNum = resulte.ToString();
            } while (orderNumbers.Contains(orderNum));
            return orderNum;
        }

        // calculate the delivery price.
        private double CalculateDeliveryPrice(double customerLat, double customerLong, double driverLat, double driverLong,
            double orderedLiters)
        {
            // Retrieve the free delivery threshold (in liters)
            double freeDeliveryThreshold = GetConstantValue("FreeDeliveryThreshold");
            // If the customer orders at least the threshold, no delivery fee is charged.
            if (orderedLiters >= freeDeliveryThreshold)
            {
                return 0.0;
            }
            // Calculate the distance in meters using the Haversine formula.
            double distanceInMeters = HaversineDistance(customerLat, customerLong, driverLat, driverLong);

            // adjust the distance if needed (e.g., unit conversion)
            double distanceConversionFactor = GetConstantValue("DistanceConversionFactor");
            double effectiveDistance = distanceInMeters * distanceConversionFactor;

            // Retrieve constant values
            double chargePerMeter = GetConstantValue("DeliveryChargePerMeter");
            double minCharge = GetConstantValue("MinimumDeliveryCharge");
            double maxCharge = GetConstantValue("MaximumDeliveryCharge");
            double fuelSurchargePercentage = GetConstantValue("FuelSurchargePercentage");

            // Calculate the base price
            double basePrice = effectiveDistance * chargePerMeter;

            // Enforce the minimum and maximum charges
            double price = Math.Max(basePrice, minCharge);
            price = Math.Min(price, maxCharge);

            // Apply the fuel surcharge percentage
            double finalPrice = price + (price * fuelSurchargePercentage / 100.0);

            return finalPrice;
        }

        private double GetConstantValue(string key)
        {
            return _unitOfWork._orderRepository.GetConstant(key);
        }
        // Haversine formula to calculate distance between two points specified by latitude and longitude.
        private double HaversineDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const double EarthRadiusKm = 6371.0; // Earth's radius in kilometers

            double dLat = DegreesToRadians(lat2 - lat1);
            double dLon = DegreesToRadians(lon2 - lon1);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(DegreesToRadians(lat1)) * Math.Cos(DegreesToRadians(lat2)) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            // Distance in kilometers, convert to meters
            double distanceInMeters = EarthRadiusKm * c * 1000;
            return distanceInMeters;
        }
        private double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180.0;
        }

    }
}
