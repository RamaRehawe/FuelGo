﻿using AutoMapper;
using FuelGo.Dto;
using FuelGo.Models;

namespace FuelGo.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, ReqRegisterCustomerDto>().ReverseMap();
            CreateMap<User, ResRegisterCustomerDto>().ReverseMap();
        }
    }
}
