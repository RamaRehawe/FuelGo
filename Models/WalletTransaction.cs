namespace FuelGo.Models
{
    public class WalletTransaction
    {
        public int Id { get; set; }
        public int WalletId { get; set; } // Foreign key to Wallet
        public int MadeBy { get; set; } // User ID who made the transaction
        public double Amount { get; set; } // Can be positive (credit) or negative (debit)
        public User User { get; set; }
        public Wallet Wallet { get; set; }
    }
}
