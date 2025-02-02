namespace FuelGo.Models
{
    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StatusTypeId { get; set; }
        public ICollection<Admin> Admins { get; set; }
        public ICollection<Driver> Drivers { get; set; }
        public ICollection<Order> Orders { get; set; }
        public StatusType StatusType { get; set; }
    }
}
