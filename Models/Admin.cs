namespace FuelGo.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CenterId { get; set; }
        public int StatusId { get; set; }
        public User User { get; set; }
        public Center Center { get; set; }
        public Status Status { get; set; }
        public ICollection<TruckHandover> TruckHandovers { get; set; }
    }
}
