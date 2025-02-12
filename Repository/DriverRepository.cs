using FuelGo.Data;
using FuelGo.Inerfaces;
using FuelGo.Models;
using Microsoft.EntityFrameworkCore;

namespace FuelGo.Repository
{
    public class DriverRepository :BaseRepository, IDriverRepository
    {
        public DriverRepository(DataContext context) : base(context)
        {
        }

        public ICollection<Order> GetPendingOrders(int statusId)
        {
            return _context.Orders.Where(o => o.StatusId == statusId).ToList();
        }

        
    }
}
