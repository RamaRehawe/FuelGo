namespace FuelGo.Dto
{
    public class ReqPlaceCarOrderDto
    {
        public double Lat { get; set; }
        public double Long { get; set; }
        public string LocationDescription { get; set; }
        public int NeighborhoodId { get; set; }
        public int CityId { get; set; }
        public int FuelTypeId { get; set; }
        public double OrderedQuantity { get; set; }
        public int CustomerCarId { get; set; }
    }
}
