namespace FuelGo.Models
{
    public class CenterService
    {
        public int Id { get; set; }
        public int CenterId { get; set; }
        public int NeighborhoodId { get; set; }
        public int Index { get; set; }
        public Center Center { get; set; }
        public Neighborhood Neighborhood { get; set; }
    }
}
