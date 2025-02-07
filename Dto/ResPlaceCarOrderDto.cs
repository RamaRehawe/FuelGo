namespace FuelGo.Dto
{
    public class ResPlaceCarOrderDto
    {
        public DateTime Date { get; set; }
        public string OrderNumber { get; set; }
        public string LocationDescription { get; set; }
        public string NeighborhoodName { get; set; }
        public string CityName { get; set; }
        public string FuelTypeName { get; set; }
        public double OrderedQuantity { get; set; }
        public double FuelPrice { get; set; }
        public string CustomerCarBrand { get; set; } // Nullable
        public string StatusName { get; set; }
    }
}
