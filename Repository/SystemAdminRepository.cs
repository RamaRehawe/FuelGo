using FuelGo.Data;
using FuelGo.Inerfaces;
using FuelGo.Models;

namespace FuelGo.Repository
{
    public class SystemAdminRepository : BaseRepository, ISystemAdminRepository
    {
        public SystemAdminRepository(DataContext context) : base(context)
        {
        }

        public bool AddAdmin(User user)
        {
            _context.Add(user);
            _context.SaveChanges();
            var admin = new Admin { UserId = user.Id };
            _context.Add(admin);
            return Save();
        }

        public Admin GetAdminById(int id)
        {
            return _context.Admins.Where(a => a.UserId == id).FirstOrDefault();
        }
    }
}
