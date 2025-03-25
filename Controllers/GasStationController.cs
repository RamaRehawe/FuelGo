using AutoMapper;
using FuelGo.Inerfaces;
using FuelGo.Models;
using FuelGo.Services;
using Microsoft.AspNetCore.Mvc;

namespace FuelGo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GasStationController : BaseController
    {
        private readonly IMapper _mapper;
        public GasStationController(UserInfoService userInfoService, IUnitOfWork unitOfWork, IMapper mapper) : 
            base(userInfoService, unitOfWork)
        {
            _mapper = mapper;
        }

        [HttpGet("get-gas-stations")]
        [ProducesResponseType(200)]
        public IActionResult GetGasStations()
        {
            var stations = _unitOfWork._gasStationRepository.GetGasStations();
            var ResStations = _mapper.Map<List<GasStation>>(stations);
            return Ok(ResStations);
        }
    }
}
