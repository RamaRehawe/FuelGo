using FuelGo.Data;
using FuelGo.Inerfaces;
using FuelGo.Models;

namespace FuelGo.Repository
{
    public class OrderRepository : BaseRepository, IOrderRepository
    {
        public OrderRepository(DataContext context) : base(context)
        {
        }

        public bool AddOrder(Order order)
        {
            _context.Add(order);
            return Save();
        }

        public string GetCarBrand(int? carId)
        {
            return _context.CustomerCars.Where(cc => cc.Id == carId).FirstOrDefault().Brand;
        }

        public string GetCityName(int neighborhoodId)
        {
            var neighborhood = _context.Neighborhoods.Where(n => n.Id == neighborhoodId).FirstOrDefault();
            return _context.Cities.Where(c => c.Id == neighborhood.CityId).FirstOrDefault().Name;
        }

        public int GetCustomerId(int userId)
        {
            return _context.Customers.Where(c => c.UserId == userId).FirstOrDefault().Id;
        }

        public string GetFuelName(int fuelTypeId)
        {
            return _context.FuelTypes.Where(f => f.Id == fuelTypeId).FirstOrDefault().Name;
        }

        public string GetNeighborhoodName(int neighborhoodId)
        {
            return _context.Neighborhoods.Where(n => n.Id == neighborhoodId).FirstOrDefault().Name;
        }

        public ICollection<Status> GetStatuses()
        {
            return _context.Statuses.OrderBy(s => s.Id).ToList();
        }
    }
}
