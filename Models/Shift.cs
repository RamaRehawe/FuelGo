namespace FuelGo.Models
{
    public class Shift
    {
        public int Id { get; set; }
        public string ShiftName { get; set; } 
        public double StartTime { get; set; }
        public double EndTime { get; set; }
        public string? WorkingDays { get; set; }
        public string? HolidayDays { get; set; }
        public ICollection<Driver> Drivers { get; set; }
    }
}
