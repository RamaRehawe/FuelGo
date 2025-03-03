using FuelGo.Models;

namespace FuelGo.Inerfaces
{
    public interface ICityRepository : IBaseRepository
    {
        ICollection<City> GetCities();
    }
}
