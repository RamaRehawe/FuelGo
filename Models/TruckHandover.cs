namespace FuelGo.Models
{
    public class TruckHandover
    {
        public int Id { get; set; }
        public int TruckId { get; set; }
        public double CargoQuantity { get; set; }
        public double FuelQuantity { get; set; }
        public double CargoVarience { get; set; }
        public double FuelVarience { get; set; }
        public double Money { get; set; }
        public int DriverId { get; set; }
        public int AdminId { get; set; }
        public Truck Truck { get; set; }
        public Driver Driver { get; set; }
        public Admin Admin { get; set; }
    }
}
