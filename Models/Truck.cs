namespace FuelGo.Models
{
    public class Truck
    {
        public int Id { get; set; }
        public int CenterId { get; set; }
        public string PlateNumber { get; set; }
        public int? DriverId { get; set; } // Nullable, as Driver_Id can be null
        public double Lat { get; set; }
        public double Long { get; set; }
        public double FuelTankCapacity { get; set; }
        public double CargoTankCapacity { get; set; }
        public double FuelTankFullCapacity { get; set; }
        public double CargoTankFullCapacity { get; set; }
        public int FuelTankTypeId { get; set; }
        public int CargoTankTypeId { get; set; }
        public Center Center { get; set; }
        public Driver Driver { get; set; }
        public FuelType FuelType { get; set; }
        public ICollection<Driver> Drivers { get; set; }
        public ICollection<TruckTankRefill> TruckTankRefills { get; set; }
        public ICollection<TruckHandover> TruckHandovers { get; set; }
    }
}
