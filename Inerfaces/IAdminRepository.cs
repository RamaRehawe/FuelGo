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
        Admin GetAdminByUserId(int userId);
        FuelDetail GetFuelByCenterAndFuelId(int centerId, int fuelTypeId);
        ICollection<Order> GetOrdersByCenterId(int centerId);
        ICollection<Order> GetOrdersByCenterIdAndStatusId(int centerId, int statusId);
        ICollection<Driver> GetDriversByCenter(int centerId);
        ICollection<Truck> GetTrucksByCenter(int centerId);
        Truck GetTruckByPlateNumber(string plateNumber);
    }
}
