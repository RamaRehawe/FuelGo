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
            return Save();
        }

        public void AddDriver(Driver driver)
        {
            _context.Add(driver);
            _context.SaveChanges();
        }

        public bool AddTruck(Truck truck)
        {
            _context.Add(truck);
            return Save();
        }

        public Admin GetAdminByUserId(int userId)
        {
            return _context.Admins.Where(a => a.UserId == userId).FirstOrDefault();
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

        public FuelDetail GetFuelByCenterAndFuelId(int centerId, int fuelTypeId)
        {
            return _context.FuelDetails.Where(fd => fd.FuelTypeId == fuelTypeId && fd.CenterId == centerId).FirstOrDefault();
        }

        public ICollection<Shift> GetShifts()
        {
            return _context.Shifts.OrderBy(s => s.Id).ToList();
        }

        public ICollection<Status> GetStatuses()
        {
            return _context.Statuses.OrderBy(s => s.Id).ToList();
        }

        public ICollection<Truck> GetTrucks()
        {
            return _context.Trucks.OrderBy(t => t.Id).ToList();
        }
    }
}
