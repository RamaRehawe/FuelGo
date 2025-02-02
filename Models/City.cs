namespace FuelGo.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Neighborhood> Neighborhoods { get; set; }
    }
}
