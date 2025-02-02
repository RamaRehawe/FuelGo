namespace FuelGo.Models
{
    public class Neighborhood
    {
        public int Id { get; set; }
        public int City_Id { get; set; }
        public string Name { get; set; }
        public City City { get; set; }
        public ICollection<Center> Centers { get; set; }
        public ICollection<CenterService> CenterServices { get; set; }
        public ICollection<GasStation> GasStations { get; set; }
        public ICollection<CustomerApartment> CustomerApartments { get; set; }
        public ICollection<Order> Orders { get; set; }

    }
}
