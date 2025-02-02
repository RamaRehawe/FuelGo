namespace FuelGo.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Order_Number { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public string Location_Description { get; set; }
        public int Neighborhood_Id { get; set; }
        public int Fuel_Type_Id { get; set; }
        public double Ordered_Quantity { get; set; }
        public double? Final_Quantity { get; set; } // Nullable
        public double? Final_Price { get; set; } // Nullable
        public bool Is_It_Urgent { get; set; }
        public int? Customer_Car_Id { get; set; } // Nullable
        public int? Customer_Apartment_Id { get; set; } // Nullable
        public int Customer_Id { get; set; }
        public int? Driver_Id { get; set; } // Nullable
        public int Status_Id { get; set; }
        public bool? Is_Active { get; set; } // Nullable
        public string Auth_Code { get; set; }
        public Neighborhood Neighborhood { get; set; }
        public FuelType FuelType { get; set; }
        public CustomerCar CustomerCar { get; set; }
        public CustomerApartment CustomerApartment { get; set; }
        public Customer Customer { get; set; }
        public Driver Driver { get; set; }
        public Status Status { get; set; }
    }
}
