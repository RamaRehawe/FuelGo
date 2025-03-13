using FuelGo.Models;

namespace FuelGo.Inerfaces
{
    public interface IFuelDetailsRepository : IBaseRepository
    {
        ICollection<FuelDetail> GetFuelDetails();
    }
}
