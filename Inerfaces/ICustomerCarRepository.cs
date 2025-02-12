using FuelGo.Models;

namespace FuelGo.Inerfaces
{
    public interface ICustomerCarRepository : IBaseRepository
    {
        bool AddCar(CustomerCar car);
        ICollection<CustomerCar> GetCars();
    }
}
