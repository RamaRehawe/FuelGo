namespace FuelGo.Dto
{
    public class ReqTruckAddingDto
    {
        public string PlateNumber { get; set; }
        public double FuelTankCapacity { get; set; }
        public double CargoTankCapacity { get; set; }
        public double FuelTankFullCapacity { get; set; }
        public double CargoTankFullCapacity { get; set; }
        public int FuelTankTypeId { get; set; }
        public int CargoTankTypeId { get; set; }
    }
}
