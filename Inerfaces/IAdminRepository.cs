using FuelGo.Models;

namespace FuelGo.Inerfaces
{
    public interface IAdminRepository : IBaseRepository
    {
        bool AddDriver(User user);
        void AddDriver(Driver driver);
        Center GetCenterByAdminId(int adminId);
        ICollection<Shift> GetShifts();
        ICollection<Truck> GetTrucks();
        Driver GetDriverByUserId(int id);
        bool AddTruck(Truck truck);
        ICollection<Status> GetStatuses();

    }
}
