namespace FuelGo.Dto
{
    public class ResPropretiesDto
    {
        public ICollection<ResGetCarDto> CustomerCars { get; set; }
        public ICollection<ResAddApartmentDto> CustomerApartments { get; set; }
    }
}
