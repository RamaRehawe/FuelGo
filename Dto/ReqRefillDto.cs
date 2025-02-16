namespace FuelGo.Dto
{
    public class ReqRefillDto
    {
        public double QuantityCargoRefill { get; set; }
        public double QuantityFuelRefill { get; set; }
        public double Price { get; set; }
        public int GasStationId { get; set; }
    }
}
