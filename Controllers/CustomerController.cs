using AutoMapper;
using FuelGo.Dto;
using FuelGo.Inerfaces;
using FuelGo.Models;
using Microsoft.AspNetCore.Mvc;

namespace FuelGo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
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

            var customer = _customerRepository.GetUsers()
                .Where(c => c.Phone == register.Phone).FirstOrDefault();
            if(customer != null)
            {
                ModelState.AddModelError("", "User already exists");
                return StatusCode(422, ModelState);
            }
            var customerMap = _mapper.Map<User>(register);
            customerMap.CreatedAt = DateTime.Now;
            customerMap.Role = "Customer";
            if(!_customerRepository.RegisterCustomer(customerMap))
            {
                ModelState.AddModelError("", "Somthing went wrong while saving");
                return StatusCode(500, ModelState);
            }
            var resCustomer = _mapper.Map<ResRegisterCustomerDto>(customerMap);
            return Ok("Successfully added");
                
        }
    }
}
