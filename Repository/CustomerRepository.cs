using FuelGo.Data;
using FuelGo.Inerfaces;
using FuelGo.Models;
using Microsoft.EntityFrameworkCore;

namespace FuelGo.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DataContext _context;
        public CustomerRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Order> GetOrders(int customerId)
        {
            return _context.Orders.Where(o => o.CustomerId == customerId)
                .Include(o => o.Neighborhood)
                    .ThenInclude(n => n.City)
                .Include(o => o.FuelType)
                .Include(o => o.CustomerCar)
                .Include(o => o.CustomerApartment)
                .ToList();
        }

        public Customer GetPropretiesByUser(int userId)
        {
            return _context.Customers.AsNoTracking()
                .Where(c => c.UserId == userId).
                Include(c => c.CustomerCars).
                Include(c => c.CustomerApartments)
                    .ThenInclude(ca => ca.Neighborhood)
                        .ThenInclude(n => n.City)
                .FirstOrDefault()!;
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
