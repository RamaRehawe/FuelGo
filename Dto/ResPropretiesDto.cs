namespace FuelGo.Dto
{
    public class ResPropretiesDto
    {
        public ICollection<AddCarDto> CustomerCars { get; set; }
        public ICollection<ResAddApartmentDto> CustomerApartments { get; set; }
    }
}
