using FuelGo.Models;

namespace FuelGo.Inerfaces
{
    public interface ISystemAdminRepository : IBaseRepository
    {
        bool AddAdmin(User user);
        Admin GetAdminById(int id);
        ICollection<Center> GetCenters();
        bool AddCenter(Center center);
        bool AddFuelDetailsForCenter(int centerId);
        ICollection<Status> GetStatuses();
        void AddAdmin(Admin admin);
        ICollection<Order> GetOrdersByStatus(int statusId);
        ICollection<Order> GetOrders();
        int GetNeighborhoodIdByCenterId(int centerId);
        ICollection<Admin> GetAdminsByCenterId(int centerId);
    }
}
