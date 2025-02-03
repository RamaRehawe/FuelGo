using FuelGo.Models;

namespace FuelGo.Inerfaces
{
    public interface IUserRepository : IBaseRepository
    {
        User GetUserByPhone(string phone);
        Task<bool> UpdateTokenByPhoneAsync(string phone, string token);
    }
}
