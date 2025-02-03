using FuelGo.Data;
using FuelGo.Inerfaces;
using FuelGo.Models;
using Microsoft.EntityFrameworkCore;

namespace FuelGo.Repository
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(DataContext context) : base(context)
        {
        }

        public User GetUserByPhone(string phone)
        {
            return _context.Users.FirstOrDefault(u => u.Phone == phone);
        }

        public async Task<bool> UpdateTokenByPhoneAsync(string phone, string token)
        {
            var user = await _context.Users.Where(u => u.Phone == phone).FirstAsync();
            user.JwtToken = token;
            _context.Users.Update(user);
            return Save();
        }
    }
}
