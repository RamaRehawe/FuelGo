﻿using FuelGo.Data;
using FuelGo.Inerfaces;
using FuelGo.Models;
using Microsoft.EntityFrameworkCore;

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

        public Order GetActiveOrderByCustomerId(int customerId)
        {
            return _context.Orders.Where(o => o.IsActive == true && o.CustomerId == customerId)
                .Include(o => o.FuelType)
                .FirstOrDefault();
        }

        public Order GetActiveOrderByDriverId(int driverId)
        {
            return _context.Orders.Where(o => o.IsActive == true && o.DriverId == driverId).FirstOrDefault();
        }

        public CustomerApartment GetApartmentById(int? id)
        {
            return _context.CustomerApartments.Where(ca => ca.Id == id).FirstOrDefault();
        }

        public string GetCarBrand(int? carId)
        {
            return _context.CustomerCars.Where(cc => cc.Id == carId).FirstOrDefault().Brand;
        }

        public Center GetCenterByCityId(int cityId, int? neighborhoodId = null)
        {
            return _context.Centers.Where(c => c.Neighborhood.CityId == cityId && (c.NeighborhoodId == neighborhoodId || neighborhoodId == null))
                .Include(c => c.Neighborhood).ThenInclude(c => c.City)
                .FirstOrDefault();
                
        }

        public City GetCityByNeighborhood(int neighborhoodId)
        {
            return _context.Neighborhoods
                .Include(n => n.City)
                .Where(n => n.Id == neighborhoodId)
                .Select(n => n.City)
                .FirstOrDefault();
        }

        public string GetCityName(int neighborhoodId)
        {
            var neighborhood = _context.Neighborhoods.Where(n => n.Id == neighborhoodId).FirstOrDefault();
            return _context.Cities.Where(c => c.Id == neighborhood.CityId).FirstOrDefault().Name;
        }

        public double GetConstant(string key)
        {
            return _context.ConstantDictionaries.Where(cd => cd.Key == key).FirstOrDefault().Value;
        }

        public int GetCustomerId(int userId)
        {
            return _context.Customers.Where(c => c.UserId == userId).FirstOrDefault().Id;
        }

        public Driver GetDriver(int userId)
        {
            return _context.Drivers.Where(d => d.UserId == userId).FirstOrDefault();
        }

        public int GetDriverId(int userId)
        {
            return _context.Drivers.Where(d => d.UserId == userId).FirstOrDefault().Id;
        }

        public string GetFuelName(int fuelTypeId)
        {
            return _context.FuelTypes.Where(f => f.Id == fuelTypeId).FirstOrDefault().Name;
        }

        public double GetFuelPrice(int fuelTypeId, int centerId)
        {
            return _context.FuelDetails.Where(fd => fd.FuelTypeId == fuelTypeId && fd.CenterId == centerId).FirstOrDefault().Price;
        }

        public string GetNeighborhoodName(int neighborhoodId)
        {
            return _context.Neighborhoods.Where(n => n.Id == neighborhoodId).FirstOrDefault().Name;
        }

        public Order GetOrder(string orderNumber)
        {
            return _context.Orders.Where(o => o.OrderNumber == orderNumber)
                .Include(o => o.CustomerApartment).ThenInclude(ca => ca.Customer)
                .Include(o => o.CustomerCar).ThenInclude(cc => cc.Customer)
                .Include(o =>o.Customer).ThenInclude(c => c.User)
                .FirstOrDefault();
        }

        public ICollection<Status> GetStatuses()
        {
            return _context.Statuses.OrderBy(s => s.Id).ToList();
        }

        public Truck GetTruck(int? truckId)
        {
            return _context.Trucks.Where(t => t.Id == truckId).FirstOrDefault();
        }

        public bool UpdateOrder(Order order)
        {
            _context.Update(order);
            return Save();
        }
    }
}
