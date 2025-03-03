namespace FuelGo.Inerfaces
{
    public interface IUnitOfWork
    {
        IAdminRepository _adminRepository { get;  }
        ICustomerRepository _customerRepository { get; }
        IOrderRepository _orderRepository { get; }
        ISystemAdminRepository _systemAdminRepository { get; }
        IUserRepository _userRepository { get; }
        IDriverRepository _driverRepository { get; }
        ICustomerCarRepository _customerCarRepository { get; }
        ICustomerApartmentRepository _customerApartmentRepository { get; }
        IWalletRepository _walletRepository { get; }
        ITankRefillRepository _tankRefillRepository { get; }
        IConstantDictionaryRepository _constantDictionaryRepository { get; }
        ICityRepository _cityRepository { get; }
        INeighborhoodRepository _neighborhoodRepository { get; }
        void Commit();
    }
}
