using FuelGo.Models;

namespace FuelGo.Inerfaces
{
    public interface IGasStationRepository : IBaseRepository
    {
        ICollection<GasStation> GetGasStations();
    }
}
