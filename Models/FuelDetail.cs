namespace FuelGo.Models
{
    public class FuelDetail
    {
        public int Id { get; set; }
        public int Fuel_Type_Id { get; set; }
        public int Center_Id { get; set; }
        public double Price { get; set; }
        public FuelType FuelType { get; set; }
        public Center Center { get; set; }
    }
}
