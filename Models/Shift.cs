namespace FuelGo.Models
{
    public class Shift
    {
        public int Id { get; set; }
        public string Shift_Name { get; set; } 
        public double Start_Time { get; set; }
        public double End_Time { get; set; }
        public string? Working_Days { get; set; }
        public string? Holiday_Days { get; set; }
        public ICollection<Driver> Drivers { get; set; }
    }
}
