using FuelGo.Data;
using FuelGo.Inerfaces;
using FuelGo.Models;

namespace FuelGo.Repository
{
    public class WalletRepository : BaseRepository, IWalletRepository
    {
        public WalletRepository(DataContext context) : base(context)
        {
        }

        public void AddWallet(Wallet wallet)
        {
            _context.Add(wallet);
            _context.SaveChanges();
        }

        public Wallet GetWalletByCustomerId(int customerId)
        {
            return _context.Wallets.Where(w => w.UserId == customerId).FirstOrDefault();
        }

        public Wallet GetWalletByUserId(int userId)
        {
            return _context.Wallets.Where(w => w.UserId == userId).FirstOrDefault();
        }
    }
}
