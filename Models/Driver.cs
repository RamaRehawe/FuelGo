namespace FuelGo.Models
{
    public class Driver
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? ShiftId { get; set; }
        public int StatusId { get; set; }
        public int? TruckId { get; set; }
        public int CenterId { get; set; }
        public bool? IsDriving { get; set; }
        public User User { get; set; }
        public Shift Shift { get; set; }
        public Status Status { get; set; }
        public Truck Truck { get; set; }
        public Center Center { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<TruckTankRefill> TruckTankRefills { get; set; }
        public ICollection<TruckHandover> TruckHandovers { get; set; }
    }
}
