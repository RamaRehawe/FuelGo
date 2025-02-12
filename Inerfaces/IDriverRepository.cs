using FuelGo.Models;

namespace FuelGo.Inerfaces
{
    public interface IDriverRepository : IBaseRepository
    {
        ICollection<Order> GetPendingOrders(int statusId);
        Order GetActiveOrderByDriverId(int driverId);
        void UpdateOrder(Order order);
    }
}
