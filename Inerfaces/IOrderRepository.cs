using FuelGo.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
        double GetConstant(string key);
        double GetFuelPrice(int fuelTypeId);
        Order GetOrder(string orderNumber);
    }
}
