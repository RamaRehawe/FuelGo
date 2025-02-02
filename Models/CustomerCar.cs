namespace FuelGo.Models
{
    public class CustomerCar
    {
        public int Id { get; set; }
        public int Customer_Id { get; set; }
        public string Plate_Number { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string Phone { get; set; }
        public bool IsDeleted { get; set; } // Soft delete flag
        public Customer Customer { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
