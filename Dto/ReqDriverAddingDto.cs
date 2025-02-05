namespace FuelGo.Dto
{
    public class ReqDriverAddingDto
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int? ShiftId { get; set; }
        public int? TruckId { get; set; }
    }
}
