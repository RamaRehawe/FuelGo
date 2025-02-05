using FuelGo.Data;
using FuelGo.Inerfaces;
using FuelGo.Models;

namespace FuelGo.Repository
{
    public class AdminRepository : BaseRepository, IAdminRepository
    {
        public AdminRepository(DataContext context) : base(context)
        {
        }

        public bool AddDriver(User user)
        {
            _context.Add(user);
            _context.SaveChanges();
            var driver = new Driver { UserId = user.Id };
            _context.Add(driver);
            return Save();
        }

        public bool AddTruck(Truck truck)
        {
            _context.Add(truck);
            return Save();
        }

        public Center GetCenterByAdminId(int adminId)
        {
            var admin = _context.Admins.Where(a => a.UserId == adminId).FirstOrDefault();
            return _context.Centers.Where(c => c.Id == admin.CenterId).FirstOrDefault();
        }

        public Driver GetDriverByUserId(int id)
        {
            return _context.Drivers.Where(d => d.UserId == id).FirstOrDefault();
        }

        public ICollection<Shift> GetShifts()
        {
            return _context.Shifts.OrderBy(s => s.Id).ToList();
        }

        public ICollection<Truck> GetTrucks()
        {
            return _context.Trucks.OrderBy(t => t.Id).ToList();
        }
    }
}
