using FuelGo.Data;
using FuelGo.Inerfaces;
using FuelGo.Models;

namespace FuelGo.Repository
{
    public class CityRepository : BaseRepository, ICityRepository
    {
        public CityRepository(DataContext context) : base(context)
        {
        }

        public ICollection<City> GetCities()
        {
            return _context.Cities.OrderBy(c => c.Name).ToList();
        }
    }
}
