using FuelGo.Models;

namespace FuelGo.Inerfaces
{
    public interface IOrderRepository : IBaseRepository
    {
        ICollection<Status> GetStatuses();
        bool AddOrder(Order order);
        int GetCustomerId(int userId);
        int GetDriverId(int userId);
        string GetNeighborhoodName(int neighborhoodId);
        string GetCityName(int neighborhoodId);
        string GetFuelName(int fuelTypeId);
        string GetCarBrand(int? carId);
        double GetConstant(string key);
        double GetFuelPrice(int fuelTypeId);
        Order GetOrder(string orderNumber);
        Truck GetTruck(int? truckId);
        Driver GetDriver(int userId);
        bool UpdateOrder(Order order);
        Order GetActiveOrderByCustomerId(int customerId);
        CustomerApartment GetApartmentById(int? id);
    }
}
