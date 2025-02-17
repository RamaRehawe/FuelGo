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
    public class CustomerController : BaseController
    {
        private readonly IMapper _mapper;

        public CustomerController(UserInfoService userInfoService, IUnitOfWork unitOfWork, IMapper mapper) : 
            base(userInfoService, unitOfWork)
        {
            _mapper = mapper;
        }

        [HttpPost("Register")]
        [ProducesResponseType(201, Type = typeof(ResRegisterCustomerDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        public IActionResult RegisterCustomer(ReqRegisterCustomerDto register)
        {
            if (register == null)
                return BadRequest(ModelState);

            var customer = _unitOfWork._customerRepository.GetUsers()
                .Where(c => c.Phone == register.Phone).FirstOrDefault();
            if(customer != null)
            {
                ModelState.AddModelError("", "User already exists");
                return StatusCode(422, ModelState);
            }
            var customerMap = _mapper.Map<User>(register);
            customerMap.CreatedAt = DateTime.Now;
            customerMap.Role = "Customer";
            if(!_unitOfWork._customerRepository.RegisterCustomer(customerMap))
            {
                ModelState.AddModelError("", "Somthing went wrong while saving");
                return StatusCode(500, ModelState);
            }
            var resCustomer = _mapper.Map<ResRegisterCustomerDto>(customerMap);
            int statusCode = 200; // Example
            return Ok(new ResponseDto<ResRegisterCustomerDto>
            {
                StatusCode = statusCode,
                Data = resCustomer
            });


        }

        [HttpGet("get-my-properties")]
        [ProducesResponseType(200)]
        [Authorize(Roles = "Customer")]
        public IActionResult GetMYProperties()
        {

            var custProp = _mapper.Map<ResPropretiesDto>(
                _unitOfWork._customerRepository.GetPropretiesByUser(base.GetActiveUser()!.Id)
                );
            if (custProp == null)
                return NotFound();
            
            return Ok(custProp);
        }

        [HttpGet("get-my-orders")]
        [Authorize(Roles = "Customer")]
        [ProducesResponseType(200)]
        public IActionResult GetMyOrders()
        {
            var customerId = _unitOfWork._orderRepository.GetCustomerId(base.GetActiveUser()!.Id);
            var orders = _unitOfWork._customerRepository.GetOrders(customerId);
            var resOrders = _mapper.Map<List<ResOrderDto>>(orders);
            return Ok(resOrders);
        }
    }
}
