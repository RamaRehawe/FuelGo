namespace FuelGo.Models
{
    public class TruckHandover
    {
        public int Id { get; set; }
        public int Truck_Id { get; set; }
        public double Cargo_Quantity { get; set; }
        public double Fuel_Quantity { get; set; }
        public double Cargo_Varience { get; set; }
        public double Fuel_Varience { get; set; }
        public double Money { get; set; }
        public int Driver_Id { get; set; }
        public int Admin_Id { get; set; }
        public Truck Truck { get; set; }
        public Driver Driver { get; set; }
        public Admin Admin { get; set; }
    }
}
