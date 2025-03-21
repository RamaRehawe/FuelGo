using FuelGo.Data;
using FuelGo.Inerfaces;
using FuelGo.Models;
using Microsoft.EntityFrameworkCore;

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
            var admin = _context.Admins.Where(a => a.Id == adminId).FirstOrDefault();
            return _context.Centers.Where(c => c.Id == admin.CenterId).FirstOrDefault();
        }

        public Driver GetDriverByUserId(int id)
        {
            return _context.Drivers.Where(d => d.UserId == id).FirstOrDefault();
        }

        public ICollection<Driver> GetDriversByCenter(int centerId)
        {
            return _context.Drivers.Where(d => d.CenterId == centerId)
                .Include(d => d.User)
                .ToList();
        }

        public FuelDetail GetFuelByCenterAndFuelId(int centerId, int fuelTypeId)
        {
            return _context.FuelDetails.Where(fd => fd.CenterId == centerId && fd.FuelTypeId == fuelTypeId).FirstOrDefault();
        }

        public ICollection<Order> GetOrdersByCenterId(int centerId)
        {
            return _context.Orders.Where(o => o.Driver.CenterId == centerId)
                .Include(o => o.Neighborhood)
                .Include(o => o.FuelType)
                .Include(o => o.CustomerApartment)
                .Include(o => o.CustomerCar)
                .OrderBy(o => o.Id).ToList();
        }

        public ICollection<Shift> GetShifts()
        {
            return _context.Shifts.OrderBy(s => s.Id).ToList();
        }

        public ICollection<Status> GetStatuses()
        {
            return _context.Statuses.OrderBy(s => s.Id).ToList();
        }

        public Truck GetTruckByPlateNumber(string plateNumber)
        {
            return _context.Trucks.Where(t => t.PlateNumber == plateNumber).FirstOrDefault();
        }

        public ICollection<Truck> GetTrucks()
        {
            return _context.Trucks.OrderBy(t => t.Id).ToList();
        }

        public ICollection<Truck> GetTrucksByCenter(int centerId)
        {
            return _context.Trucks.Where(t => t.CenterId == centerId)
                .Include(t => t.FuelType)
                .ToList();
        }
    }
}
