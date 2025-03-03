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
            CreateMap<User, ReqEditPasswordDto>().ReverseMap();
            CreateMap<TruckTankRefill, ReqRefillDto>().ReverseMap();
            CreateMap<TruckTankRefill, ReqRefillCargoDto>().ReverseMap();
            CreateMap<TruckTankRefill, ReqRefillFuelDto>().ReverseMap();
            CreateMap<FuelDetail, ReqEditFuelPriceDto>().ReverseMap();
            CreateMap<Driver, ResDriversDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.User.Phone))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.IsDriving, opt => opt.MapFrom(src => src.IsDriving));
            CreateMap<Truck, ResTrucksDto>()
                .ForMember(dest => dest.FuelTankTypeName, opt => opt.MapFrom(src => src.FuelType.Name))
                .ForMember(dest => dest.CargoTankTypeName, opt => opt.MapFrom(src => src.FuelType.Name));
            CreateMap<Shift, ResShiftsDto>().ReverseMap();
            CreateMap<WalletTransaction, ReqChargeWalletDto>().ReverseMap();
            CreateMap<ConstantDictionary, ReqConstantValueDto>().ReverseMap();
            CreateMap<City, ResCityDto>().ReverseMap();
        }
    }
}
