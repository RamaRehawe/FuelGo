namespace FuelGo.Models
{
    public class CustomerApartment
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int NeighborhoodId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public string LocationDescription { get; set; }
        public bool IsDeleted { get; set; } // Soft delete flag
        public Customer Customer { get; set; }
        public Neighborhood Neighborhood { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
