using FuelGo.Models;

namespace FuelGo.Inerfaces
{
    public interface ICustomerRepository
    {
        ICollection<User> GetUsers();
        bool UserExist(int id);
        bool RegisterCustomer(User user);
        Customer GetPropretiesByUser(int userId);
        bool Save();
    }
}
