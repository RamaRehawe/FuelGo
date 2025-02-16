using FuelGo.Data;
using FuelGo.Inerfaces;
using FuelGo.Models;

namespace FuelGo.Repository
{
    public class TankRefillRepository : BaseRepository, ITankRefillRepository
    {
        public TankRefillRepository(DataContext context) : base(context)
        {
        }

        public bool Refill(TruckTankRefill truckTankRefill)
        {
            _context.Add(truckTankRefill);
            return Save();
        }
    }
}
