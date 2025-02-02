using FuelGo.Models;
using Microsoft.EntityFrameworkCore;

namespace FuelGo.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Center> Centers { get; set; }
        public DbSet<CenterService> CenterServices { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<ConstantDictionary> ConstantDictionaries { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerApartment> CustomerApartments { get; set; }
        public DbSet<CustomerCar> CustomerCars { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<FuelDetail> FuelDetails { get; set; }
        public DbSet<FuelType> FuelTypes { get; set; }
        public DbSet<GasStation> GasStations { get; set; }
        public DbSet<Neighborhood> Neighborhoods { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<StatusType> StatusTypes { get; set; }
        public DbSet<SystemAdmin> SystemAdmins { get; set; }
        public DbSet<Truck> Trucks { get; set; }
        public DbSet<TruckHandover> TruckHandovers { get; set; }
        public DbSet<TruckTankRefill> TruckTankRefills { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<WalletTransaction> WalletTransactions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Unique Contraints
            // User
            modelBuilder.Entity<User>()
                .HasIndex(u => new { u.Email, u.IsNotDeleted })
                .IsUnique();
            modelBuilder.Entity<User>()
                .HasIndex(u => new { u.Phone, u.IsNotDeleted })
                .IsUnique();

            // SystemAdmin
            modelBuilder.Entity<SystemAdmin>()
                .HasIndex(sa => sa.UserId)
                .IsUnique();

            // Admin
            modelBuilder.Entity<Admin>()
                .HasIndex(a => a.UserId)
                .IsUnique();

            // Driver
            modelBuilder.Entity<Driver>()
                .HasIndex(d => new { d.TruckId, d.ShiftId })
                .IsUnique();
            modelBuilder.Entity<Driver>()
                .HasIndex(d => d.UserId)
                .IsUnique();

            // Customer
            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.UserId)
                .IsUnique();

            // Center
            modelBuilder.Entity<Center>()
                .HasIndex(c => new { c.NeighborhoodId, c.Name })
                .IsUnique();

            // CenterService
            modelBuilder.Entity<CenterService>()
                .HasIndex(cs => new { cs.CenterId, cs.NeighborhoodId })
                .IsUnique();

            // City
            modelBuilder.Entity<City>()
                .HasIndex(c => c.Name)
                .IsUnique();

            // Neighborhood
            modelBuilder.Entity<Neighborhood>()
                .HasIndex(n => new { n.CityId, n.Name })
                .IsUnique();

            // GasStation
            modelBuilder.Entity<GasStation>()
                .HasIndex(gs => new { gs.NeighborhoodId, gs.Name })
                .IsUnique();

            // FuelType
            modelBuilder.Entity<FuelType>()
                .HasIndex(ft => ft.Name)
                .IsUnique();

            // FuelDetail
            modelBuilder.Entity<FuelDetail>()
                .HasIndex(fd => new { fd.FuelTypeId, fd.CenterId })
                .IsUnique();

            // Truck
            modelBuilder.Entity<Truck>()
                .HasIndex(t => t.PlateNumber)
                .IsUnique();

            // ConstantDictionary
            modelBuilder.Entity<ConstantDictionary>()
                .HasIndex(c => c.Key)
                .IsUnique();

            // CustomerCar
            modelBuilder.Entity<CustomerCar>()
                .HasIndex(cc => new { cc.PlateNumber, cc.CustomerId })
                .IsUnique();

            // Wallet
            modelBuilder.Entity<Wallet>()
                 .HasIndex(w => w.UserId)
                 .IsUnique();

            // Order
            modelBuilder.Entity<Order>()
                .HasIndex(o => new { o.CustomerCarId, o.IsActive })
                .IsUnique();
            modelBuilder.Entity<Order>()
                .HasIndex(o => new { o.CustomerApartmentId, o.IsActive })
                .IsUnique();
            modelBuilder.Entity<Order>()
                .HasIndex(o => new { o.DriverId, o.IsActive })
                .IsUnique();

            // Status
            modelBuilder.Entity<Status>()
                .HasIndex(s => new { s.Name, s.StatusTypeId })
                .IsUnique();

            // Relations
            // one to one
            modelBuilder.Entity<User>()
                .HasOne(u => u.SystemAdmin)
                .WithOne(sa => sa.User)
                .HasForeignKey<SystemAdmin>(sa => sa.UserId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Admin)
                .WithOne(a => a.User)
                .HasForeignKey<Admin>(a => a.UserId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Driver)
                .WithOne(d => d.User)
                .HasForeignKey<Driver>(d => d.UserId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Customer)
                .WithOne(c => c.User)
                .HasForeignKey<Customer>(c => c.UserId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Wallet)
                .WithOne(w => w.User)
                .HasForeignKey<Wallet>(w => w.UserId);



            // one to many
            modelBuilder.Entity<Admin>()
                .HasOne(a => a.Center)
                .WithMany(c => c.Admins)
                .HasForeignKey(a => a.CenterId);

            modelBuilder.Entity<Admin>()
                .HasOne(a => a.Status)
                .WithMany(s => s.Admins)
                .HasForeignKey(a => a.StatusId);

            modelBuilder.Entity<Driver>()
                .HasOne(d => d.Shift)
                .WithMany(s => s.Drivers)
                .HasForeignKey(d => d.ShiftId);

            modelBuilder.Entity<Driver>()
                .HasOne(d => d.Status)
                .WithMany(s => s.Drivers)
                .HasForeignKey(d => d.StatusId);

            modelBuilder.Entity<Driver>()
                .HasOne(d => d.Truck)
                .WithMany(t => t.Drivers)
                .HasForeignKey(d => d.TruckId);

            modelBuilder.Entity<Driver>()
                .HasOne(d => d.Center)
                .WithMany(c => c.Drivers)
                .HasForeignKey(d => d.CenterId);

            modelBuilder.Entity<Center>()
                .HasOne(c => c.Neighborhood)
                .WithMany(n => n.Centers)
                .HasForeignKey(c => c.NeighborhoodId);

            modelBuilder.Entity<CenterService>()
                .HasOne(cs => cs.Center)
                .WithMany(c => c.CenterServices)
                .HasForeignKey(cs => cs.CenterId);

            modelBuilder.Entity<CenterService>()
                .HasOne(cs => cs.Neighborhood)
                .WithMany(n => n.CenterServices)
                .HasForeignKey(cs => cs.NeighborhoodId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Neighborhood>()
                .HasOne(n => n.City)
                .WithMany(c => c.Neighborhoods)
                .HasForeignKey(n => n.CityId);

            modelBuilder.Entity<GasStation>()
                .HasOne(gs => gs.Neighborhood)
                .WithMany(n => n.GasStations)
                .HasForeignKey(gs => gs.NeighborhoodId);

            modelBuilder.Entity<FuelDetail>()
                .HasOne(fd => fd.FuelType)
                .WithMany(ft => ft.FuelDetails)
                .HasForeignKey(fd => fd.FuelTypeId);

            modelBuilder.Entity<FuelDetail>()
                .HasOne(fd => fd.Center)
                .WithMany(c => c.FuelDetails)
                .HasForeignKey(fd => fd.CenterId);

            modelBuilder.Entity<Truck>()
                .HasOne(t => t.Center)
                .WithMany(c => c.Trucks)
                .HasForeignKey(t => t.CenterId);

            modelBuilder.Entity<Truck>()
                .HasOne(t => t.Driver)
                .WithMany(d => d.Trucks)
                .HasForeignKey(t => t.DriverId);

            modelBuilder.Entity<Truck>()
                .HasOne(t => t.FuelType)
                .WithMany(ft => ft.Trucks)
                .HasForeignKey(t => t.FuelTankTypeId);

            modelBuilder.Entity<Truck>()
                .HasOne(t => t.FuelType)
                .WithMany(ft => ft.Trucks)
                .HasForeignKey(t => t.CargoTankTypeId);

            modelBuilder.Entity<CustomerCar>()
                .HasOne(cc => cc.Customer)
                .WithMany(c => c.CustomerCars)
                .HasForeignKey(cc => cc.CustomerId);

            modelBuilder.Entity<CustomerApartment>()
                .HasOne(ca => ca.Customer)
                .WithMany(c => c.CustomerApartments)
                .HasForeignKey(ca => ca.CustomerId);

            modelBuilder.Entity<CustomerApartment>()
                .HasOne(ca => ca.Neighborhood)
                .WithMany(n => n.CustomerApartments)
                .HasForeignKey(ca => ca.NeighborhoodId);

            modelBuilder.Entity<WalletTransaction>()
                .HasOne(wt => wt.Wallet)
                .WithMany(w => w.WalletTransactions)
                .HasForeignKey(wt => wt.WalletId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<WalletTransaction>()
                .HasOne(wt => wt.User)
                .WithMany(u => u.WalletTransactions)
                .HasForeignKey(wt => wt.MadeBy);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Neighborhood)
                .WithMany(n => n.Orders)
                .HasForeignKey(o => o.NeighborhoodId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.FuelType)
                .WithMany(ft => ft.Orders)
                .HasForeignKey(o => o.FuelTypeId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.CustomerCar)
                .WithMany(cc => cc.Orders)
                .HasForeignKey(o => o.CustomerCarId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.CustomerApartment)
                .WithMany(ca => ca.Orders)
                .HasForeignKey(o => o.CustomerApartmentId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Driver)
                .WithMany(d => d.Orders)
                .HasForeignKey(o => o.DriverId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Status)
                .WithMany(s => s.Orders)
                .HasForeignKey(o => o.StatusId);

            modelBuilder.Entity<Status>()
                .HasOne(s => s.StatusType)
                .WithMany(st => st.Statuses)
                .HasForeignKey(s => s.StatusTypeId);

            modelBuilder.Entity<TruckTankRefill>()
                .HasOne(ttr => ttr.Truck)
                .WithMany(t => t.TruckTankRefills)
                .HasForeignKey(ttr => ttr.TruckId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TruckTankRefill>()
                .HasOne(ttr => ttr.GasStation)
                .WithMany(gs => gs.TruckTankRefills)
                .HasForeignKey(ttr => ttr.GasStationId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TruckTankRefill>()
                .HasOne(ttr => ttr.Driver)
                .WithMany(d => d.TruckTankRefills)
                .HasForeignKey(ttr => ttr.DriverId);

            modelBuilder.Entity<TruckHandover>()
                .HasOne(th => th.Truck)
                .WithMany(t => t.TruckHandovers)
                .HasForeignKey(th => th.TruckId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TruckHandover>()
                .HasOne(th => th.Driver)
                .WithMany(d => d.TruckHandovers)
                .HasForeignKey(th => th.DriverId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TruckHandover>()
                .HasOne(th => th.Admin)
                .WithMany(a => a.TruckHandovers)
                .HasForeignKey(th => th.AdminId);
        }


    }
}
