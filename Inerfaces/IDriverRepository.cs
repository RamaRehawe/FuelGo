using FuelGo.Models;

namespace FuelGo.Inerfaces
{
    public interface IDriverRepository : IBaseRepository
    {
        ICollection<Order> GetPendingOrders(int statusId);
    }
}
