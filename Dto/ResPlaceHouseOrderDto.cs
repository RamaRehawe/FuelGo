using FuelGo.Models;

namespace FuelGo.Dto
{
    public class ResPlaceHouseOrderDto
    {
        public DateTime Date { get; set; }
        public string OrderNumber { get; set; }
        public string LocationDescription { get; set; }
        public string NeighborhoodName { get; set; }
        public string CityName { get; set; }
        public string FuelTypeName { get; set; }
        public double OrderedQuantity { get; set; }
        public string StatusName { get; set; }
        public int StatusId { get; set; }
        public CustomerApartment Apartment { get; set; }
        public double? TotalPrice { get; set; }
        public double Fee { get; set; }
    }
}
