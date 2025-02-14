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
            CreateMap<CustomerCar, AddCarDto>().ReverseMap();
            CreateMap<CustomerApartment, ReqAddApartmentDto>().ReverseMap();
            CreateMap<CustomerApartment, ResAddApartmentDto>()
                .ForMember(dest => dest.NeighborhoodName, opt => opt.MapFrom(src => src.Neighborhood.Name))
                .ForMember(dest => dest.CityName, opt => 
                opt.MapFrom(src => src.Neighborhood.City != null ? src.Neighborhood.City.Name : null));
            CreateMap<Order, ResPlaceHouseOrderDto>().ReverseMap();
            CreateMap<Order, ReqPlaceHouseOrderDto>().ReverseMap();
            CreateMap<Order, ResOrderDto>().ReverseMap();
            CreateMap<Customer, ResPropretiesDto>().ReverseMap();

        }
    }
}
