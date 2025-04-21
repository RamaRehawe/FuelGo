namespace FuelGo.Dto
{
    public class ResPendingOrdersDto
    {
        public DateTime Date { get; set; }
        public string OrderNumber { get; set; }
        public double? CustomerLat { get; set; }
        public double? CustomerLong { get; set; }
        public string LocationDescription { get; set; }
        public int NeighborhoodId { get; set; }
        public int FuelTypeId { get; set; }
        public double OrderedQuantity { get; set; }
        public int? CustomerCarId { get; set; }
        public int? CustomerAppartmentId { get; set; }
    }
}
