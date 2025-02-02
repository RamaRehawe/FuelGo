namespace FuelGo.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string OrderNumber { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public string LocationDescription { get; set; }
        public int NeighborhoodId { get; set; }
        public int FuelTypeId { get; set; }
        public double OrderedQuantity { get; set; }
        public double? FinalQuantity { get; set; } // Nullable
        public double? FinalPrice { get; set; } // Nullable
        public bool IsItUrgent { get; set; }
        public int? CustomerCarId { get; set; } // Nullable
        public int? CustomerApartmentId { get; set; } // Nullable
        public int CustomerId { get; set; }
        public int? DriverId { get; set; } // Nullable
        public int StatusId { get; set; }
        public bool? IsActive { get; set; } // Nullable
        public string AuthCode { get; set; }
        public Neighborhood Neighborhood { get; set; }
        public FuelType FuelType { get; set; }
        public CustomerCar CustomerCar { get; set; }
        public CustomerApartment CustomerApartment { get; set; }
        public Customer Customer { get; set; }
        public Driver Driver { get; set; }
        public Status Status { get; set; }
    }
}
