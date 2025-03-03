using AutoMapper;
using FuelGo.Dto;
using FuelGo.Inerfaces;
using FuelGo.Models;
using FuelGo.Services;
using Microsoft.AspNetCore.Mvc;

namespace FuelGo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : BaseController
    {
        private readonly IMapper _mapper;
        public CityController(UserInfoService userInfoService, IUnitOfWork unitOfWork, IMapper mapper) : 
            base(userInfoService, unitOfWork)
        {
            _mapper = mapper;
        }
        [HttpGet("get-cities")]
        [ProducesResponseType(200)]
        public IActionResult GetCities()
        {
            var cities = _mapper.Map<List<ResCityDto>>(_unitOfWork._cityRepository.GetCities());
            return Ok(cities);
        }
    }
}
