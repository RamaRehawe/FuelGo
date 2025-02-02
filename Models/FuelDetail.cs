namespace FuelGo.Models
{
    public class FuelDetail
    {
        public int Id { get; set; }
        public int FuelTypeId { get; set; }
        public int CenterId { get; set; }
        public double Price { get; set; }
        public FuelType FuelType { get; set; }
        public Center Center { get; set; }
    }
}
