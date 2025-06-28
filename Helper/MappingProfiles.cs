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

            CreateMap<Order, ResPendingOrdersDto>()
                .ForMember(dest => dest.CustomerCarId, opt => opt.MapFrom(src => src.CustomerCarId)) // Explicitly map
                .ForMember(dest => dest.CustomerAppartmentId, opt => opt.MapFrom(src => src.CustomerApartmentId)) // Explicitly map
                .ReverseMap();

            CreateMap<CustomerCar, AddCarDto>().ReverseMap();
            
            CreateMap<CustomerCar, ResGetCarDto>().ReverseMap();
            
            CreateMap<CustomerApartment, ReqAddApartmentDto>().ReverseMap();
            
            CreateMap<CustomerApartment, ResAddApartmentDto>()
                .ForMember(dest => dest.NeighborhoodName, opt => opt.MapFrom(src => src.Neighborhood.Name))
                .ForMember(dest => dest.CityName, opt => 
                opt.MapFrom(src => src.Neighborhood.City != null ? src.Neighborhood.City.Name : null));
            
            CreateMap<Order, ResPlaceHouseOrderDto>().ReverseMap();
            
            CreateMap<Order, ReqPlaceHouseOrderDto>().ReverseMap();
            
            CreateMap<Order, ResOrderDto>()
                .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status.Name))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.User.Name))
                .ForMember(dest => dest.CustomerPhone, opt => opt.MapFrom(src => src.Customer.User.Phone))
                .ForMember(dest => dest.DriverName, opt => opt.MapFrom(src => src.Driver != null ? src.Driver.User.Name : null))
                .ForMember(dest => dest.DriverPhone, opt => opt.MapFrom(src => src.Driver != null ? src.Driver.User.Phone : null))
                .ForMember(dest => dest.CenterName, opt => opt.MapFrom(src => src.Driver != null ? src.Driver.Center.Name : null));

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
            
            CreateMap<Neighborhood, ResNeighborhoodDto>().ReverseMap();
            
            CreateMap<Center, ResCentersDto>().ReverseMap();

            CreateMap<Admin, ResAdminDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.User.Phone))
                .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status.Name));

            CreateMap<ResAdminDto, Admin>()
                .ForMember(dest => dest.User, opt => opt.Ignore()) // Prevent overwriting User object
                .ForMember(dest => dest.Status, opt => opt.Ignore()); // Prevent overwriting Status object

            CreateMap<FuelDetail, ResFuelDetailsDto>()
                .ForMember(dest => dest.FuelTypeName, opt => opt.MapFrom(src => src.FuelType.Name))
                .ForMember(dest => dest.CenterName, opt => opt.MapFrom(src => src.Center.Name));

            CreateMap<ResFuelDetailsDto, FuelDetail>()
                .ForMember(dest => dest.FuelType, opt => opt.Ignore()) // Prevent accidental overwrites
                .ForMember(dest => dest.Center, opt => opt.Ignore());

            CreateMap<GasStation, ResGasStationsDto>().ReverseMap();

            CreateMap<Status, ResStatusDto>().ReverseMap();

            CreateMap<Order, ReqGetOrdersDto>().ReverseMap();
            CreateMap<User, ResProfileDto>().ReverseMap();
        }
    }
}
