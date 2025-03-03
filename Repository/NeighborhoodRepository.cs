using FuelGo.Data;
using FuelGo.Inerfaces;
using FuelGo.Models;

namespace FuelGo.Repository
{
    public class NeighborhoodRepository : BaseRepository, INeighborhoodRepository
    {
        public NeighborhoodRepository(DataContext context) : base(context)
        {
        }

        public ICollection<Neighborhood> GetNeighborhoodsByCity(int cityId)
        {
            return _context.Neighborhoods.Where(n => n.CityId == cityId).OrderBy(n => n.Name).ToList();
        }
    }
}
