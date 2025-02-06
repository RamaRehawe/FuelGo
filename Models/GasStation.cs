namespace FuelGo.Models
{
    public class GasStation
    {
        public int Id { get; set; }
        public int NeighborhoodId { get; set; }
        public string Name { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public string LocationDescription { get; set; }
        public Neighborhood Neighborhood { get; set; }
        public ICollection<TruckTankRefill> TruckTankRefills { get; set; }
    }
}
