using FuelGo.Data;
using FuelGo.Inerfaces;
using FuelGo.Models;

namespace FuelGo.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DataContext _context;
        public CustomerRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<User> GetUsers()
        {
            return _context.Users.OrderBy(u => u.Id).ToList();
        }

        public bool RegisterCustomer(User user)
        {
            _context.Add(user);
            _context.SaveChanges();
            var customer = new Customer { UserId = user.Id };
            _context.Add(customer);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false; 
        }

        public bool UserExist(int id)
        {
            return _context.Users.Any(u => u.Id == id);
        }
    }
}
