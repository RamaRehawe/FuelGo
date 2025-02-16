using FuelGo.Data;
using FuelGo.Inerfaces;
using FuelGo.Models;
using Microsoft.EntityFrameworkCore;

namespace FuelGo.Repository
{
    public class DriverRepository :BaseRepository, IDriverRepository
    {
        public DriverRepository(DataContext context) : base(context)
        {
        }

        public Order GetActiveOrderByDriverId(int driverId)
        {
            return _context.Orders.Where(o => o.DriverId == driverId && o.IsActive == true).FirstOrDefault();
        }

        public ICollection<Order> GetOrders(int driverId)
        {
            return _context.Orders.Where(o => o.DriverId == driverId)
                .Include(o => o.Neighborhood)
                    .ThenInclude(n => n.City)
                .Include(o => o.FuelType)
                .Include(o => o.CustomerCar)
                .Include(o => o.CustomerApartment)
                .ToList();
        }

        public ICollection<Order> GetPendingOrders(int statusId)
        {
            return _context.Orders.Where(o => o.StatusId == statusId).ToList();
        }

        public void UpdateOrder(Order order)
        {
            order.IsActive = null;
            _context.Update(order);
            _context.SaveChanges();
        }
    }
}
