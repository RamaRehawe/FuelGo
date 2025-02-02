namespace FuelGo.Models
{
    public class TruckTankRefill
    {
        public int Id { get; set; }
        public int Truck_Id { get; set; }
        public double Quantity_Cargo_Refill { get; set; }
        public double Quantity_Fuel_Refill { get; set; }
        public double Price { get; set; }
        public int Gas_Station_Id { get; set; }
        public int Driver_Id { get; set; }
        public Truck Truck { get; set; }
        public GasStation GasStation { get; set; }
        public Driver Driver { get; set; }
    }
}
