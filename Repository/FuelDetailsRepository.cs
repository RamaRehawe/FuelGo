using FuelGo.Data;
using FuelGo.Inerfaces;
using FuelGo.Models;
using Microsoft.EntityFrameworkCore;

namespace FuelGo.Repository
{
    public class FuelDetailsRepository : BaseRepository, IFuelDetailsRepository
    {
        public FuelDetailsRepository(DataContext context) : base(context)
        {
        }

        public ICollection<FuelDetail> GetFuelDetails()
        {
            return _context.FuelDetails
                .Include(fd => fd.Center)
                .Include(fd => fd.FuelType)
                .ToList();
        }
    }
}
