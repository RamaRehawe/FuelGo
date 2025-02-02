namespace FuelGo.Models
{
    public class StatusType
    {
        public int Id { get; set; }
        public string Name { get; set; } // "Order", "Admin", "Driver"
        public Status Status { get; set; }
    }
}
