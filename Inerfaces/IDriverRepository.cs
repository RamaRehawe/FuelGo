using FuelGo.Models;

namespace FuelGo.Inerfaces
{
    public interface IDriverRepository : IBaseRepository
    {
        ICollection<Order> GetPendingOrders(int statusId, Center center);
        Order GetActiveOrderByDriverId(int driverId);
        void UpdateOrder(Order order);
        ICollection<Order> GetOrders(int driverId);
        Status GetDriverStatus(int userId);
    }
}
