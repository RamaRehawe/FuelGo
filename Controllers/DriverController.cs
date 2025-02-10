using AutoMapper;
using FuelGo.Dto;
using FuelGo.Inerfaces;
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
            var orders = _unitOfWork._driverRepository.GetPendingOrders(statusId);
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
            var statusId = _unitOfWork._orderRepository.GetStatuses().Where(s => s.Name == "متاح").FirstOrDefault();
            driver.Status = statusId;
            _unitOfWork.Commit();
            return Ok("Started");
        }
    }
}
