using FuelGo.Data;
using FuelGo.Inerfaces;
using FuelGo.Models;

namespace FuelGo.Repository
{
    public class GasStationRepository : BaseRepository, IGasStationRepository
    {
        public GasStationRepository(DataContext context) : base(context)
        {
        }

        public ICollection<GasStation> GetGasStations()
        {
            return _context.GasStations.OrderBy(gs => gs.Id).ToList();
        }
    }
}
