using FuelGo.Data;
using FuelGo.Inerfaces;
using FuelGo.Models;

namespace FuelGo.Repository
{
    public class CustomerCarRepository : BaseRepository, ICustomerCarRepository
    {
        public CustomerCarRepository(DataContext context) : base(context)
        {
        }

        public bool AddCar(CustomerCar car)
        {
            _context.Add(car);
            return Save();
        }

        public ICollection<CustomerCar> GetCars()
        {
            return _context.CustomerCars.OrderBy(cc => cc.Id).ToList();
        }
    }
}
