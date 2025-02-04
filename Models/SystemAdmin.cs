namespace FuelGo.Models
{
    public class SystemAdmin
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; } 
        public User User { get; set; }
    }

}
