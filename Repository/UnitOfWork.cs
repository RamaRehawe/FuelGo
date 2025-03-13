using FuelGo.Data;
using FuelGo.Inerfaces;

namespace FuelGo.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        public IAdminRepository _adminRepository { get; private set; }
        public ICustomerRepository _customerRepository { get; private set; }
        public IOrderRepository _orderRepository { get; private set; }
        public ISystemAdminRepository _systemAdminRepository { get; private set; }
        public IUserRepository _userRepository { get; private set; }
        public IDriverRepository _driverRepository { get; private set; }
        public ICustomerCarRepository _customerCarRepository { get; private set; }
        public ICustomerApartmentRepository _customerApartmentRepository { get; private set; }
        public IWalletRepository _walletRepository { get; private set; }
        public ITankRefillRepository _tankRefillRepository { get; private set; }
        public IConstantDictionaryRepository _constantDictionaryRepository { get; private set; }
        public ICityRepository _cityRepository { get; private set; }
        public INeighborhoodRepository _neighborhoodRepository { get; private set; }
        public IFuelDetailsRepository _fuelDetailsRepository { get; private set; }
        public UnitOfWork(DataContext context, IAdminRepository adminRepository, 
            ICustomerRepository customerRepository, IOrderRepository orderRepository, 
            ISystemAdminRepository systemAdminRepository, IUserRepository userRepository, 
            IDriverRepository driverRepository, ICustomerCarRepository customerCarRepository,
            ICustomerApartmentRepository customerApartmentRepository, IWalletRepository walletRepository,
            ITankRefillRepository tankRefillRepository, IConstantDictionaryRepository constantDictionaryRepository,
            ICityRepository cityRepository, INeighborhoodRepository neighborhoodRepository,
            IFuelDetailsRepository fuelDetailsRepository)
        {
            _context = context;
            _adminRepository = adminRepository;
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
            _systemAdminRepository = systemAdminRepository;
            _userRepository = userRepository;
            _driverRepository = driverRepository;
            _customerCarRepository = customerCarRepository;
            _customerApartmentRepository = customerApartmentRepository;
            _walletRepository = walletRepository;
            _tankRefillRepository = tankRefillRepository;
            _constantDictionaryRepository = constantDictionaryRepository;
            _cityRepository = cityRepository;
            _neighborhoodRepository = neighborhoodRepository;
            _fuelDetailsRepository = fuelDetailsRepository;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}
