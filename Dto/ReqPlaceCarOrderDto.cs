namespace FuelGo.Dto
{
    public class ReqPlaceCarOrderDto
    {
        public double CustomerLat { get; set; }
        public double CustomerLong { get; set; }
        public string LocationDescription { get; set; }
        public int NeighborhoodId { get; set; }
        public int CityId { get; set; }
        public int FuelTypeId { get; set; }
        public double OrderedQuantity { get; set; }
        public int CustomerCarId { get; set; }
    }
}
