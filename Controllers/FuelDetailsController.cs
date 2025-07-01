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
    public class FuelDetailsController : BaseController
    {
        private readonly IMapper _mapper;
        public FuelDetailsController(UserInfoService userInfoService, IUnitOfWork unitOfWork, IMapper mapper) : 
            base(userInfoService, unitOfWork)
        {
            _mapper = mapper;
        }
        [HttpGet("get-fuel-details-by-center")]
        [ProducesResponseType(200)]
        public IActionResult GetFuelDetailsByCenter()
        {
            var admin = _unitOfWork._adminRepository.GetAdminByUserId(base.GetActiveUser()!.Id);
            var center = _unitOfWork._adminRepository.GetCenterByAdminId(admin.Id);
            var fuelDeatails = _unitOfWork._fuelDetailsRepository.GetFuelDetailsByCenter(center.Id);
            var resDetails = _mapper.Map<List<ResFuelDetailsDto>>(fuelDeatails);
            return Ok(resDetails);
        }

        [HttpGet("get-fuel-details")]
        [ProducesResponseType(200)]
        public IActionResult GetFuelDetails()
        {
            var fuelDeatails = _unitOfWork._fuelDetailsRepository.GetFuelDetails();
            var resDetails = _mapper.Map<List<ResFuelDetailsDto>>(fuelDeatails);
            return Ok(resDetails);
        }
    }
}
