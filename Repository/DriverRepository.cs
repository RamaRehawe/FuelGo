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
            return _context.Orders.Where(o => o.DriverId == driverId && o.IsActive == true)
                .Include(o => o.CustomerApartment).ThenInclude(ca => ca.Customer)
                .Include(o => o.CustomerCar).ThenInclude(cc => cc.Customer)
                .Include(o => o.Customer).ThenInclude(c => c.User)
                .FirstOrDefault();
        }

        public Status GetDriverStatus(int userId)
        {
            var driver = _context.Drivers.Where(d => d.UserId == userId).FirstOrDefault();
            return _context.Statuses.Where(s => s.Id == driver.StatusId).FirstOrDefault();
        }

        public ICollection<Order> GetOrders(int driverId)
        {
            return _context.Orders.Where(o => o.DriverId == driverId)
                .Include(o => o.Neighborhood)
                    .ThenInclude(n => n.City)
                .Include(o => o.FuelType)
                .Include(o => o.CustomerCar)
                .Include(o => o.CustomerApartment)
                .Include(o => o.Status)
                .Include(o => o.Driver).ThenInclude(d => d.User)
                .Include(o => o.Customer).ThenInclude(c => c.User)
                .ToList();
        }

        public ICollection<Order> GetPendingOrders(int statusId, Center center)
        {
            var cityId = _context.Neighborhoods.Where(n => n.Id == center.NeighborhoodId).FirstOrDefault().CityId;
            return _context.Orders.Where(o => o.StatusId == statusId && o.Neighborhood.CityId == cityId)
                .Include(o => o.CustomerApartment)
                .Include(o => o.CustomerCar)
                .Include(o => o.Neighborhood)
                .ToList();
        }

        public void UpdateOrder(Order order)
        {
            order.IsActive = null;
            _context.Update(order);
            _context.SaveChanges();
        }
    }
}
