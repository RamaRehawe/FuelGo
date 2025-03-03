using FuelGo.Models;

namespace FuelGo.Inerfaces
{
    public interface INeighborhoodRepository : IBaseRepository
    {
        ICollection<Neighborhood> GetNeighborhoodsByCity(int cityId);
    }
}
