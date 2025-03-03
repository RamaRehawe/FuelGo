using AutoMapper;
using FuelGo.Dto;
using FuelGo.Inerfaces;
using FuelGo.Services;
using Microsoft.AspNetCore.Mvc;

namespace FuelGo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NeighborhoodController : BaseController
    {
        private readonly IMapper _mapper;
        public NeighborhoodController(UserInfoService userInfoService, IUnitOfWork unitOfWork, IMapper mapper) : 
            base(userInfoService, unitOfWork)
        {
            _mapper = mapper;
        }

        [HttpGet("get-neighborhood")]
        [ProducesResponseType(200)]
        public IActionResult GetNeighbohoods(int cityId)
        {
            var neighborhoods = _mapper.Map<List<ResNeighborhoodDto>>(
                _unitOfWork._neighborhoodRepository.GetNeighborhoodsByCity(cityId));
            return Ok(neighborhoods);
        }
    }
}
