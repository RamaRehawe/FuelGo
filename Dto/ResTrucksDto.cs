namespace FuelGo.Dto
{
    public class ResTrucksDto
    {
        public int Id { get; set; }
        public string PlateNumber { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public double FuelTankCapacity { get; set; }
        public double CargoTankCapacity { get; set; }
        public double FuelTankFullCapacity { get; set; }
        public double CargoTankFullCapacity { get; set; }
        public string FuelTankTypeName { get; set; }
        public string CargoTankTypeName { get; set; }
    }
}
