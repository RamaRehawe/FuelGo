namespace FuelGo.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime UpdatedAt { get; set; } 
        public DateTime CreatedAt { get; set; } 
        public bool? IsNotDeleted { get; set; }
        public string? JwtToken { get; set; }
        public SystemAdmin SystemAdmin { get; set; }
        public Admin Admin { get; set; }
        public Driver Driver { get; set; }
        public Customer Customer { get; set; }
        public Wallet Wallet { get; set; }
        public ICollection<WalletTransaction> WalletTransactions { get; set; }
    }
}
