﻿using AutoMapper;
using FuelGo.Dto;
using FuelGo.Inerfaces;
using FuelGo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FuelGo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConstantDictionaryController : BaseController
    {
        private readonly IMapper _mapper;
        public ConstantDictionaryController(UserInfoService userInfoService, IUnitOfWork unitOfWork, IMapper mapper) : 
            base(userInfoService, unitOfWork)
        {
            _mapper = mapper;
        }

        [HttpPost("edit-constant-value")]
        [Authorize(Roles = "SystemAdmin")]
        [ProducesResponseType(200)]
        public IActionResult EditConstantValues(List<ReqConstantValueDto> values)
        {
            foreach (var valueDto in values)
            {
                var constant = _unitOfWork._constantDictionaryRepository.GetConstantDictionary(valueDto.Key);
                if (constant != null)
                {
                    constant.Value = valueDto.Value;
                }
            }
            _unitOfWork.Commit();
            return Ok("Constants updated successfully.");
        }

        [HttpGet("get-constant-values")]
        [ProducesResponseType(200)]
        public IActionResult GetContatntValues()
        {
            var values = _unitOfWork._constantDictionaryRepository.GetConstantDictionaries();
            return Ok(values);
        }

        [HttpPost("calculate-payment")]
        [ProducesResponseType(200)]
        public IActionResult CalculatePayment(ReqCalcPaymentDto data)
        {
            var values = _unitOfWork._constantDictionaryRepository.GetConstantDictionaries();
            var center = _unitOfWork._orderRepository.GetCenterByCityId(data.CityId);
            var fuelPrice = _unitOfWork._orderRepository.GetFuelPrice(data.FuelTypeId, center.Id);
            var fee = CalculateDeliveryPrice(data.CustomerLat, data.CustomerLong, center.Lat, center.Long,
                data.Quantity);
            var payment = fuelPrice * data.Quantity;

            return Ok(new
            {
                FuelPrice = payment,
                Fee = fee,
                Total = payment + fee
            });
        }
    }
}
