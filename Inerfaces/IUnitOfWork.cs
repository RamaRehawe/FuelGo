using FuelGo.Data;

namespace FuelGo.Inerfaces
{
    public interface IUnitOfWork
    {
        public IAdminRepository _adminRepository { get; set; }
        public ISystemAdminRepository _systemAdminRepository { get; set; }
        public ICustomerRepository _customerRepository { get; set; }
        public IUserRepository _userRepository { get; set; }

        bool Save();
    }
}
