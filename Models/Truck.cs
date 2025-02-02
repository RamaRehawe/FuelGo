namespace FuelGo.Models
{
    public class Truck
    {
        public int Id { get; set; }
        public int Center_Id { get; set; }
        public string Plate_Number { get; set; }
        public int? Driver_Id { get; set; } // Nullable, as Driver_Id can be null
        public double Lat { get; set; }
        public double Long { get; set; }
        public double Fuel_Tank_Capacity { get; set; }
        public double Cargo_Tank_Capacity { get; set; }
        public double Fuel_Tank_Full_Capacity { get; set; }
        public double Cargo_Tank_Full_Capacity { get; set; }
        public int Fuel_Tank_Type_Id { get; set; }
        public int Cargo_Tank_Type_Id { get; set; }
        public Center Center { get; set; }
        public Driver Driver { get; set; }
        public FuelType FuelType { get; set; }
        public ICollection<Driver> Drivers { get; set; }
        public ICollection<TruckTankRefill> TruckTankRefills { get; set; }
        public ICollection<TruckHandover> TruckHandovers { get; set; }
    }
}
