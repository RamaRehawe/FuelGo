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

        public ICollection<FuelDetail> GetFuelDetailsByCenter(int centerId)
        {
            return _context.FuelDetails.Where(fd => fd.CenterId == centerId)
                .Include(fd => fd.Center)
                .Include(fd => fd.FuelType)
                .ToList();
        }
    }
}
