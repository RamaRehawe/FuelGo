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
            return Save();
        }

        public void AddAdmin(Admin admin)
        {
            _context.Add(admin);
            _context.SaveChanges();
        }

        public bool AddCenter(Center center)
        {
            _context.Add(center);
            return Save();
        }

        public Admin GetAdminById(int id)
        {
            return _context.Admins.Where(a => a.UserId == id).FirstOrDefault();
        }

        public ICollection<Center> GetCenters()
        {
            return _context.Centers.OrderBy(c => c.Id).ToList();
        }

        public ICollection<Status> GetStatuses()
        {
            return _context.Statuses.OrderBy(s => s.Id).ToList();
        }
    }
}
