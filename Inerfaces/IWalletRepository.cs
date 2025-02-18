using FuelGo.Models;

namespace FuelGo.Inerfaces
{
    public interface IWalletRepository : IBaseRepository
    {
        Wallet GetWalletByUserId(int userId);
        void AddWallet(Wallet wallet);
        Wallet GetWalletByCustomerId(int customerId);
    }
}
