namespace FuelGo.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<CustomerCar> CustomerCars { get; set; }
        public ICollection<CustomerApartment> CustomerApartments { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
