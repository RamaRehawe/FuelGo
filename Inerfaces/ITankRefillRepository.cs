using FuelGo.Models;

namespace FuelGo.Inerfaces
{
    public interface ITankRefillRepository : IBaseRepository
    {
        bool Refill(TruckTankRefill truckTankRefill);
    }
}
