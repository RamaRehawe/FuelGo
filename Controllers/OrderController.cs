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
        private readonly IOrderRepository _orderRepository;
        private static Random random = new Random();
        private static HashSet<string> orderNumbers = new HashSet<string>();

        public OrderController(UserInfoService userInfoService, IUserRepository userRepository, IOrderRepository orderRepository,
            IMapper mapper) : 
            base(userInfoService, userRepository)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        [HttpPost("place-car-order")]
        [Authorize(Roles = "Customer")]
        [ProducesResponseType(201)]
        [ProducesResponseType(500)]
        public IActionResult PlaceCarOrder(ReqPlaceCarOrderDto orderData)
        {
            if (orderData == null)
                return BadRequest(ModelState);
            var orderMap = _mapper.Map<Order>(orderData);
            orderMap.Date = DateTime.Now;
            orderMap.OrderNumber = GenerateRandomCode(6);
            orderMap.IsItUrgent = false;
            orderMap.CustomerId = _orderRepository.GetCustomerId( base.GetActiveUser().Id);
            var statusId = _orderRepository.GetStatuses().Where(s => s.Name == "قيد الانتظار").FirstOrDefault().Id;
            orderMap.StatusId = statusId;
            orderMap.IsActive = true;
            orderMap.AuthCode = GenerateRandomCode(10);
            if(!_orderRepository.AddOrder(orderMap))
            {
                ModelState.AddModelError("", "Somthing went wrong while saving");
                return StatusCode(500, ModelState);
            }
            
            var neighborhoodName = _orderRepository.GetNeighborhoodName(orderMap.NeighborhoodId);
            var cityName = _orderRepository.GetCityName(orderMap.NeighborhoodId);
            var fuelName = _orderRepository.GetFuelName(orderMap.FuelTypeId);
            var carBrand = _orderRepository.GetCarBrand(orderMap.CustomerCarId);
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
    }
}
