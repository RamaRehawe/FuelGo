namespace FuelGo.Models
{
    public class Center
    {
        public int Id { get; set; }
        public int NeighborhoodId { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public string LocationDescription { get; set; }
        public ICollection<Admin> Admins { get; set; }
        public ICollection<Driver> Drivers { get; set; }
        public Neighborhood Neighborhood { get; set; }
        public ICollection<CenterService> CenterServices { get; set; }
        public ICollection<FuelDetail> FuelDetails { get; set; }
        public ICollection<Truck> Trucks { get; set; }
    }
}
