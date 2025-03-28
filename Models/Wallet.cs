﻿namespace FuelGo.Models
{
    public class Wallet
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public double Amount { get; set; }
        public string? OTP { get; set; }
        public User User { get; set; }
        public ICollection<WalletTransaction> WalletTransactions { get; set; }
    }
}
