namespace FuelGo.Dto
{
    public class ReqCalcPaymentDto
    {
        public double Quantity { get; set; }
        public int CityId { get; set; }
        public int FuelTypeId { get; set; }
        public double CustomerLat { get; set; }
        public double CustomerLong { get; set; }
    }
}
