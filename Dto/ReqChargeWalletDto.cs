namespace FuelGo.Dto
{
    public class ReqChargeWalletDto
    {
        public string Phone { get; set; }
        public string? OTP { get; set; }
        public double Amount { get; set; }
    }
}
