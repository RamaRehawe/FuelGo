using AutoMapper;
using FuelGo.Inerfaces;
using FuelGo.Services;
using Microsoft.AspNetCore.Mvc;

namespace FuelGo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : BaseController
    {
        public TestController(UserInfoService userInfoService, IUnitOfWork unitOfWork) : base(userInfoService, unitOfWork)
        {
        }

        [HttpGet("health-check")]
        public IActionResult HealthCheck()
        {
            return Ok("I am alive");
        }
        [HttpGet("test-error")]
        public IActionResult TestError()
        {
            int x = 0;
            return Ok(1 / x);
        }
        [HttpGet("test-database")]
        public IActionResult TestDataBase()
        {
            var x = _unitOfWork._adminRepository.GetShifts();

            return Ok(x);
        }
    }
}
