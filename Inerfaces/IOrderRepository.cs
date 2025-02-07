using FuelGo.Models;

namespace FuelGo.Inerfaces
{
    public interface IOrderRepository : IBaseRepository
    {
        ICollection<Status> GetStatuses();
        bool AddOrder(Order order);
        int GetCustomerId(int userId);
        string GetNeighborhoodName(int neighborhoodId);
        string GetCityName(int neighborhoodId);
        string GetFuelName(int fuelTypeId);
        string GetCarBrand(int? carId);
    }
}
