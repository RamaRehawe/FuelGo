using AutoMapper;
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
            CreateMap<User, ReqLoginDto>().ReverseMap();
            CreateMap<User, ReqDriverAddingDto>().ReverseMap();
            CreateMap<Driver, ReqDriverAddingDto>().ReverseMap();
            CreateMap<User, ResDriverAddingDto>().ReverseMap();
            CreateMap<Driver, ResDriverAddingDto>().ReverseMap();
            CreateMap<User, ReqAdminAddingDto>().ReverseMap();
            CreateMap<Admin, ReqAdminAddingDto>().ReverseMap();
            CreateMap<User, ResAdminAddingDto>().ReverseMap();
            CreateMap<Admin, ResAdminAddingDto>().ReverseMap();
            CreateMap<Center, ReqCenterAddingDto>().ReverseMap();
            CreateMap<Truck, ReqTruckAddingDto>().ReverseMap();
            CreateMap<Order, ReqPlaceCarOrderDto>().ReverseMap();
            CreateMap<Order, ReqAcceptOrderDto>().ReverseMap();
            CreateMap<Order, ResPendingOrdersDto>().ReverseMap();
            CreateMap<CustomerCar, ReqAddCarDto>().ReverseMap();
            CreateMap<CustomerCar, ResAddCarDto>().ReverseMap();
        }
    }
}
