using FuelGo.Data;
using FuelGo.Inerfaces;
using FuelGo.Models;
using Microsoft.EntityFrameworkCore;

namespace FuelGo.Repository
{
    public class SystemAdminRepository : BaseRepository, ISystemAdminRepository
    {
        public SystemAdminRepository(DataContext context) : base(context)
        {
        }

        public bool AddAdmin(User user)
        {
            _context.Add(user);
            return Save();
        }

        public void AddAdmin(Admin admin)
        {
            _context.Add(admin);
            _context.SaveChanges();
        }

        public bool AddCenter(Center center)
        {
            _context.Add(center);
            return Save();
        }

        public Admin GetAdminById(int id)
        {
            return _context.Admins.Where(a => a.UserId == id).FirstOrDefault();
        }

        public ICollection<Admin> GetAdminsByCenterId(int centerId)
        {
            return _context.Admins.Where(a => a.CenterId == centerId)
                .Include(a => a.User)
                .Include(a => a.Status)
                .ToList();
        }

        public ICollection<Center> GetCenters()
        {
            return _context.Centers.OrderBy(c => c.Id).ToList();
        }

        public int GetNeighborhoodIdByCenterId(int centerId)
        {
            var center = _context.Centers.Where(c => c.Id == centerId).FirstOrDefault();
            return center.NeighborhoodId;
        }

        public ICollection<Order> GetOrders()
        {
            return _context.Orders
                .Include(o => o.Neighborhood)
                .Include(o => o.FuelType)
                .Include(o => o.CustomerApartment)
                .Include(o => o.CustomerCar)
                .ToList();
        }

        public ICollection<Status> GetStatuses()
        {
            return _context.Statuses.OrderBy(s => s.Id).ToList();
        }
    }
}
