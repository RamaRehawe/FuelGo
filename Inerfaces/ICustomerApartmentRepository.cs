using FuelGo.Models;

namespace FuelGo.Inerfaces
{
    public interface ICustomerApartmentRepository : IBaseRepository
    {
        bool AddApartment(CustomerApartment customerApartment);
        ICollection<CustomerApartment> GetApartment();
    }
}
