namespace FuelGo.Models
{
    public class FuelType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<FuelDetail> FuelDetails { get; set; }
        public ICollection<Truck> Trucks { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
