using FuelGo.Data;
using FuelGo.Inerfaces;
using FuelGo.Models;

namespace FuelGo.Repository
{
    public class CustomerApartmentRepository : BaseRepository, ICustomerApartmentRepository
    {
        public CustomerApartmentRepository(DataContext context) : base(context)
        {
        }

        public bool AddApartment(CustomerApartment customerApartment)
        {
            _context.Add(customerApartment);
            return Save();
        }
    }
}
