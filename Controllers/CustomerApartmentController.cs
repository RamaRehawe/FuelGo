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
    public class CustomerApartmentController : BaseController
    {
        private readonly IMapper _mapper;
        public CustomerApartmentController(UserInfoService userInfoService, IUnitOfWork unitOfWork, IMapper mapper) : 
            base(userInfoService, unitOfWork)
        {
            _mapper = mapper;
        }

        [HttpPost("add-appartment")]
        [ProducesResponseType(201, Type = typeof(ResAddApartmentDto))]
        [Authorize(Roles = "Customer")]
        public IActionResult AddApartment(ReqAddApartmentDto aptData)
        {
            if(aptData == null)
                return BadRequest(ModelState);
            var aptMap = _mapper.Map<CustomerApartment>(aptData);
            aptMap.CustomerId = _unitOfWork._orderRepository.GetCustomerId(base.GetActiveUser()!.Id);
            aptMap.IsDeleted = false;
            if(!_unitOfWork._customerApartmentRepository.AddApartment(aptMap))
            {
                ModelState.AddModelError("", "Somthing went wrong while saving");
                return StatusCode(500, ModelState);
            }
            var neighborhoodName = _unitOfWork._orderRepository.GetNeighborhoodName(aptMap.NeighborhoodId);
            var cityName = _unitOfWork._orderRepository.GetCityName(aptMap.NeighborhoodId);

            var resApt = _mapper.Map<ResAddApartmentDto>(aptMap);
            resApt.NeighborhoodName = neighborhoodName;
            resApt.CityName = cityName;
            return Ok(resApt);
        }

        [HttpPatch("delete-apartment")]
        public IActionResult DeleteApartment(int aptId)
        {
            if (aptId == null)
                return BadRequest(ModelState);
            var apt = _unitOfWork._customerApartmentRepository.GetApartment().Where(a => a.Id == aptId).FirstOrDefault();
            if(apt == null)
            {
                ModelState.AddModelError("", "Apartment not found");
                return StatusCode(404, ModelState);
            }
            apt.IsDeleted = true;
            _unitOfWork.Commit();
            return Ok("Apartment Deleted Successfully");
        }
    }
}
