namespace FuelGo.Models
{
    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Status_Type_Id { get; set; }
        public ICollection<Admin> Admins { get; set; }
        public ICollection<Driver> Drivers { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<StatusType> StatusTypes { get; set; }
    }
}
