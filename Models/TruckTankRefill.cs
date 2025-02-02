namespace FuelGo.Models
{
    public class TruckTankRefill
    {
        public int Id { get; set; }
        public int TruckId { get; set; }
        public double QuantityCargoRefill { get; set; }
        public double QuantityFuelRefill { get; set; }
        public double Price { get; set; }
        public int GasStationId { get; set; }
        public int DriverId { get; set; }
        public Truck Truck { get; set; }
        public GasStation GasStation { get; set; }
        public Driver Driver { get; set; }
    }
}
