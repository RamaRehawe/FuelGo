using FuelGo.Data;
using FuelGo.Inerfaces;

namespace FuelGo.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public IAdminRepository _adminRepository { get; set; }
        public ICustomerRepository _customerRepository { get; set; }
        public IUserRepository _userRepository { get; set; }
        public ISystemAdminRepository _systemAdminRepository { get; set; }

        protected readonly DataContext _context;
        public UnitOfWork(DataContext context)
        {
            _context = context;
            _adminRepository = new AdminRepository(context);
            _customerRepository = new CustomerRepository(context);
            _userRepository = new UserRepository(context);
            _systemAdminRepository = new SystemAdminRepository(context);
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
