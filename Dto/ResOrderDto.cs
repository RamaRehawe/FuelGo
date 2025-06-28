namespace FuelGo.Dto
{
    public class ResOrderDto
    {
        public DateTime Date { get; set; }
        public string OrderNumber { get; set; }
        public string LocationDescription { get; set; }
        public string NeighborhoodName { get; set; }
        public string FuelTypeName { get; set; }
        public double OrderedQuantity { get; set; }
        public double? Price { get; set; }
        public double? FinalQuantity { get; set; }
        public double? FinalPrice { get; set; }
        public string? CustomerCarBrand { get; set; }
        public string? CustomerApartmentName { get; set; }
        public string AuthCode { get; set; }
        public string StatusName { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string? DriverName { get; set; }
        public string? DriverPhone { get; set; }
        public double? DeliveryFee { get; set; }
        public string CenterName { get; set; }
    }
}
