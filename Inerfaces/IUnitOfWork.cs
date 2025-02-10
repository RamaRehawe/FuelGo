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

        void Commit();
    }
}
