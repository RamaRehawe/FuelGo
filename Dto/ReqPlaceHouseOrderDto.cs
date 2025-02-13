namespace FuelGo.Dto
{
    public class ReqPlaceHouseOrderDto
    {
        
        public int FuelTypeId { get; set; }
        public double OrderedQuantity { get; set; }
        public int? CustomerApartmentId { get; set; }

    }
}
