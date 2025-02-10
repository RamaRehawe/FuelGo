﻿using FuelGo.Data;
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
        public UnitOfWork(DataContext context, IAdminRepository adminRepository, 
            ICustomerRepository customerRepository, IOrderRepository orderRepository, 
            ISystemAdminRepository systemAdminRepository, IUserRepository userRepository, IDriverRepository driverRepository)
        {
            _context = context;
            _adminRepository = adminRepository;
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
            _systemAdminRepository = systemAdminRepository;
            _userRepository = userRepository;
            _driverRepository = driverRepository;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}
