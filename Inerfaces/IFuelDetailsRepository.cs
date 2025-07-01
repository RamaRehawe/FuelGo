using FuelGo.Models;

namespace FuelGo.Inerfaces
{
    public interface IFuelDetailsRepository : IBaseRepository
    {
        ICollection<FuelDetail> GetFuelDetailsByCenter(int centerId);
        ICollection<FuelDetail> GetFuelDetails();
    }
}
