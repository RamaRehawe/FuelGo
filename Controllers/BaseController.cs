using FuelGo.Inerfaces;
using FuelGo.Models;
using FuelGo.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System;

namespace FuelGo.Controllers
{
    public class BaseController : Controller
    {
        protected readonly UserInfoService _userInfoService;
        protected readonly IUnitOfWork _unitOfWork;
        private static Random random = new Random();
        private static HashSet<string> orderNumbers = new HashSet<string>();

        public BaseController(UserInfoService userInfoService, IUnitOfWork unitOfWork)
        {
            _userInfoService = userInfoService;
            _unitOfWork = unitOfWork;
        }

        protected User? GetActiveUser()
        {
            var phone = _userInfoService.GetUserIdFromToken();
            var user = _unitOfWork._userRepository.GetUserByPhone(phone);
            return user;
        }

        protected string GenerateRandomCode(int length)
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
        protected double CalculateDeliveryPrice(double? customerLat, double? customerLong, double? driverLat, double? driverLong,
            double orderedLiters)
        {
            // Retrieve the free delivery threshold (in liters)
            double freeDeliveryThreshold = GetConstantValue("حد التوصيل المجاني");
            // If the customer orders at least the threshold, no delivery fee is charged.
            if (orderedLiters >= freeDeliveryThreshold)
            {
                return 0.0;
            }
            // Calculate the distance in kelo meters using the Haversine formula.
            double distanceInKeloMeters = HaversineDistance(customerLat, customerLong, driverLat, driverLong);

            // adjust the distance if needed (e.g., unit conversion)
            double distanceConversionFactor = GetConstantValue("عامل تحويل المسافات");
            double effectiveDistance = distanceInKeloMeters * distanceConversionFactor;

            // Retrieve constant values
            double chargePerMeter = GetConstantValue("رسوم التوصيل لكل كيلومتر");
            double minCharge = GetConstantValue("الحد الأدنى لرسوم التوصيل");
            double maxCharge = GetConstantValue("الحد الأقصى لرسوم التوصيل");
            double fuelSurchargePercentage = GetConstantValue("نسبة الرسوم الإضافية على الوقود");

            // Calculate the base price
            double basePrice = distanceInKeloMeters * chargePerMeter;

            // Enforce the minimum and maximum charges
            double price = Math.Max(basePrice, minCharge);
            price = Math.Min(price, maxCharge);

            // Apply the fuel surcharge percentage
            double finalPrice = price + (price * fuelSurchargePercentage / 100.0);
            Console.WriteLine(price + " " + finalPrice);
            return finalPrice;
        }

        protected double GetConstantValue(string key)
        {
            return _unitOfWork._orderRepository.GetConstant(key);
        }
        // Haversine formula to calculate distance between two points specified by latitude and longitude.
        protected double HaversineDistance(double? lat1, double? lon1, double? lat2, double? lon2)
        {
            const double EarthRadiusKm = 6371.0; // Earth's radius in kilometers

            double dLat = DegreesToRadians(lat2 - lat1);
            double dLon = DegreesToRadians(lon2 - lon1);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(DegreesToRadians(lat1)) * Math.Cos(DegreesToRadians(lat2)) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            // Distance in kilometers, convert to meters
            double distanceInKeloMeters = EarthRadiusKm * c;
            return distanceInKeloMeters;
        }
        private double DegreesToRadians(double? degrees)
        {
            return (double)(degrees * Math.PI / 180.0);
        }

        protected TimeSpan? CalculateEstimatedTime(Order order)
        {
            var statusId = _unitOfWork._orderRepository.GetStatuses().Where(s => s.Name == "قيد الانتظار").FirstOrDefault().Id;

            if (order.StatusId == statusId)
                return null; // No estimate for pending orders

            double distanceKm = GetDistance(order.DriverLat, order.DriverLong, order.CustomerLat, order.CustomerLong);
            double avgSpeedKmPerHour = 50; // Example speed (adjust as needed)

            double estimatedHours = distanceKm / avgSpeedKmPerHour;
            return TimeSpan.FromHours(estimatedHours);
        }
        protected double GetDistance(double? driverLat, double? driverLong, double? customerLat, double? customerLong)
        {
            // Use Haversine formula or Google Maps API
            return HaversineDistance(driverLat, driverLong, customerLat, customerLong);
        }
    }
}
