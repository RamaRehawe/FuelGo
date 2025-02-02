namespace FuelGo.Models
{
    public class Wallet
    {
        public int Id { get; set; }
        public int User_Id { get; set; }
        public double Amount { get; set; }
        public User User { get; set; }
        public ICollection<WalletTransaction> WalletTransactions { get; set; }
    }
}
