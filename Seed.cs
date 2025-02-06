using FuelGo.Data;
using FuelGo.Models;
using System;
using System.Collections.Generic;

namespace FuelGo
{
    public class Seed
    {
        private readonly DataContext dataContext;

        public Seed(DataContext context)
        {
            this.dataContext = context;
            SeedDataContext();
        }

        public void SeedDataContext()
        {
            // seeding shifts
            if (!dataContext.Shifts.Any())
            {
                var shifts = new List<Shift>
                {
                    new Shift { ShiftName = "Morning Shift", StartTime = 6.00, EndTime = 14.00, WorkingDays = "All-Week", HolidayDays = null },
                    new Shift { ShiftName = "Midday Shift", StartTime = 12.00, EndTime = 20.00, WorkingDays = "All-Week", HolidayDays = null },
                    new Shift { ShiftName = "Evening Shift", StartTime = 14.00, EndTime = 22.00, WorkingDays = "All-Week", HolidayDays = null },
                    new Shift { ShiftName = "Night Shift", StartTime = 20.00, EndTime = 4.00, WorkingDays = "All-Week", HolidayDays = null },
                    new Shift { ShiftName = "Graveyard Shift", StartTime = 22.00, EndTime = 6.00, WorkingDays = "All-Week", HolidayDays = null },
                };

                dataContext.Shifts.AddRange(shifts);
                dataContext.SaveChanges();
            }
            // Seeding Cities
            if (!dataContext.Cities.Any())
            {
                var cities = new List<City>
                {
                    new City { Name = "دمشق" },
                    new City { Name = "حلب" },
                    new City { Name = "حمص" },
                    new City { Name = "حماة" },
                    new City { Name = "اللاذقية" },
                    new City { Name = "طرطوس" },
                    new City { Name = "درعا" },
                    new City { Name = "السويداء" },
                    new City { Name = "الحسكة" },
                    new City { Name = "دير الزور" },
                    new City { Name = "الرقة" },
                    new City { Name = "إدلب" },
                };

                dataContext.Cities.AddRange(cities);
                        dataContext.SaveChanges();
            }

            // Seeding Neighborhoods
            if (!dataContext.Neighborhoods.Any())
            {
                var neighborhoods = new List<Neighborhood>
                {
                    // دمشق
                    new Neighborhood { CityId = 1, Name = "المزة" },
                    new Neighborhood { CityId = 1, Name = "المالكي" },
                    new Neighborhood { CityId = 1, Name = "ركن الدين" },
                    new Neighborhood { CityId = 1, Name = "باب توما" },
                    new Neighborhood { CityId = 1, Name = "كفرسوسة" },

                    // حلب
                    new Neighborhood { CityId = 2, Name = "المشارقة" },
                    new Neighborhood { CityId = 2, Name = "السليمانية" },
                    new Neighborhood { CityId = 2, Name = "حلب الجديدة" },
                    new Neighborhood { CityId = 2, Name = "الأعظمية" },
                    new Neighborhood { CityId = 2, Name = "صلاح الدين" },

                    // حمص
                    new Neighborhood { CityId = 3, Name = "الوعر" },
                    new Neighborhood { CityId = 3, Name = "باب الدريب" },
                    new Neighborhood { CityId = 3, Name = "الإنشاءات" },
                    new Neighborhood { CityId = 3, Name = "الغوطة" },
                    new Neighborhood { CityId = 3, Name = "كرم الشامي" },

                    // حماة
                    new Neighborhood { CityId = 4, Name = "الحاضر" },
                    new Neighborhood { CityId = 4, Name = "القصور" },
                    new Neighborhood { CityId = 4, Name = "الشريعة" },
                    new Neighborhood { CityId = 4, Name = "باب النهر" },
                    new Neighborhood { CityId = 4, Name = "الكيلانية" },

                    // اللاذقية
                    new Neighborhood { CityId = 5, Name = "مشروع الصليبة" },
                    new Neighborhood { CityId = 5, Name = "الرمل الجنوبي" },
                    new Neighborhood { CityId = 5, Name = "حي قنينص" },
                    new Neighborhood { CityId = 5, Name = "الصليبة" },
                    new Neighborhood { CityId = 5, Name = "حي الأمريكان" },

                    // طرطوس
                    new Neighborhood { CityId = 6, Name = "الكورنيش" },
                    new Neighborhood { CityId = 6, Name = "بانياس" },
                    new Neighborhood { CityId = 6, Name = "الشيخ بدر" },
                    new Neighborhood { CityId = 6, Name = "صافيتا" },
                    new Neighborhood { CityId = 6, Name = "الدريكيش" },

                    // درعا
                    new Neighborhood { CityId = 7, Name = "درعا البلد" },
                    new Neighborhood { CityId = 7, Name = "المخيم" },
                    new Neighborhood { CityId = 7, Name = "الصنمين" },
                    new Neighborhood { CityId = 7, Name = "ازرع" },
                    new Neighborhood { CityId = 7, Name = "الشيخ مسكين" },

                    // السويداء
                    new Neighborhood { CityId = 8, Name = "شهبا" },
                    new Neighborhood { CityId = 8, Name = "صلخد" },
                    new Neighborhood { CityId = 8, Name = "القريا" },
                    new Neighborhood { CityId = 8, Name = "عرمان" },
                    new Neighborhood { CityId = 8, Name = "الرحى" },

                    // الحسكة
                    new Neighborhood { CityId = 9, Name = "القامشلي" },
                    new Neighborhood { CityId = 9, Name = "عامودا" },
                    new Neighborhood { CityId = 9, Name = "رأس العين" },
                    new Neighborhood { CityId = 9, Name = "الدرباسية" },
                    new Neighborhood { CityId = 9, Name = "تل تمر" },

                    // دير الزور
                    new Neighborhood { CityId = 10, Name = "البوكمال" },
                    new Neighborhood { CityId = 10, Name = "الميادين" },
                    new Neighborhood { CityId = 10, Name = "الجبيلة" },
                    new Neighborhood { CityId = 10, Name = "حي القصور" },
                    new Neighborhood { CityId = 10, Name = "موحسن" },

                    // الرقة
                    new Neighborhood { CityId = 11, Name = "الدرعية" },
                    new Neighborhood { CityId = 11, Name = "حي المشلب" },
                    new Neighborhood { CityId = 11, Name = "حي الطيار" },
                    new Neighborhood { CityId = 11, Name = "الرومانية" },
                    new Neighborhood { CityId = 11, Name = "السباهية" },

                    // إدلب
                    new Neighborhood { CityId = 12, Name = "جسر الشغور" },
                    new Neighborhood { CityId = 12, Name = "معرة النعمان" },
                    new Neighborhood { CityId = 12, Name = "أريحا" },
                    new Neighborhood { CityId = 12, Name = "كفرنبل" },
                    new Neighborhood { CityId = 12, Name = "سلقين" },
                };

                dataContext.Neighborhoods.AddRange(neighborhoods);
                dataContext.SaveChanges();
            }

            // Seeding Fuel Types
            if (!dataContext.FuelTypes.Any())
            {
                var fuelTypes = new List<FuelType>
                {
                    new FuelType { Name = "بنزين" },
                    new FuelType { Name = "مازوت" }
                };

                dataContext.FuelTypes.AddRange(fuelTypes);
                dataContext.SaveChanges();
            }
            // Seeding Status Types
            if (!dataContext.StatusTypes.Any())
            {
                var statusTypes = new List<StatusType>
                {
                    new StatusType { Name = "Order" },
                    new StatusType { Name = "Admin" },
                    new StatusType { Name = "Driver" }
                };

                dataContext.StatusTypes.AddRange(statusTypes);
                dataContext.SaveChanges();
            }
            // Fetching Status Type IDs
            var orderType = dataContext.StatusTypes.FirstOrDefault(s => s.Name == "Order");
            var adminType = dataContext.StatusTypes.FirstOrDefault(s => s.Name == "Admin");
            var driverType = dataContext.StatusTypes.FirstOrDefault(s => s.Name == "Driver");

            // Seeding Statuses
            if (!dataContext.Statuses.Any())
            {
                var statuses = new List<Status>
                {
                    // Order Statuses
                    new Status { Name = "قيد الانتظار", StatusTypeId = orderType.Id }, // Pending
                    new Status { Name = "مقبول", StatusTypeId = orderType.Id  }, // Accepted
                    new Status { Name = "مرفوض", StatusTypeId = orderType.Id  }, // Rejected
                    new Status { Name = "في الطريق", StatusTypeId = orderType.Id  }, // On the Way
                    new Status { Name = "وصل للموقع", StatusTypeId = orderType.Id  }, // Arrived Location 
                    new Status { Name = "بدء تعبئة الطلب", StatusTypeId = orderType.Id  }, // Start Servicing 
                    new Status { Name = "تم التسليم", StatusTypeId = orderType.Id  }, // Delivered
                    new Status { Name = "ملغي", StatusTypeId = orderType.Id  }, // Canceled

                    // Admin Statuses
                    new Status { Name = "نشط", StatusTypeId = adminType.Id }, // Active
                    new Status { Name = "غير نشط", StatusTypeId = adminType.Id }, // Inactive
                    new Status { Name = "محظور", StatusTypeId = adminType.Id }, // Banned

                    // Driver Statuses
                    new Status { Name = "متاح", StatusTypeId = driverType.Id }, // Available
                    new Status { Name = "مشغول", StatusTypeId = driverType.Id }, // Busy
                    new Status { Name = "غير متصل", StatusTypeId = driverType.Id }, // Offline
                    new Status { Name = "في اجازة", StatusTypeId = driverType.Id } // On Leave
                };

                dataContext.Statuses.AddRange(statuses);
                dataContext.SaveChanges();
            }
            // seeding centers
            if (!dataContext.Centers.Any())
            {
                var centers = new List<Center>
                {
                    new Center { Name = "مركز دمشق", Phone = "0111234567", Lat = 33.5138, Long = 36.2765, LocationDescription = "قرب ساحة الأمويين", NeighborhoodId = 1 },
                    new Center { Name = "مركز حلب", Phone = "0219876543", Lat = 36.2021, Long = 37.1343, LocationDescription = "جانب القلعة", NeighborhoodId = 6 },
                    new Center { Name = "مركز حمص", Phone = "0314567890", Lat = 34.7324, Long = 36.7139, LocationDescription = "شارع الدبلان", NeighborhoodId = 11 },
                    new Center { Name = "مركز حماة", Phone = "0331122334", Lat = 35.1379, Long = 36.7518, LocationDescription = "جانب السوق المركزي", NeighborhoodId = 16 },
                    new Center { Name = "مركز اللاذقية", Phone = "0415566778", Lat = 35.5258, Long = 35.7822, LocationDescription = "بالقرب من الكورنيش الجنوبي", NeighborhoodId = 21 },
                };

                dataContext.Centers.AddRange(centers);
                dataContext.SaveChanges();
            }
            // Seeding Trucks
            if (!dataContext.Trucks.Any())
            {
                var trucks = new List<Truck>
                {
                    new Truck
                    {
                        CenterId = 1,
                        PlateNumber = "ABC-123",
                        Lat = 33.5138,
                        Long = 36.2765,
                        FuelTankCapacity = 5000,
                        CargoTankCapacity = 3000,
                        FuelTankFullCapacity = 5000,
                        CargoTankFullCapacity = 3000,
                        FuelTankTypeId = 1,    // assuming type id exists
                        CargoTankTypeId = 2
                    },
                    new Truck
                    {
                        CenterId = 2,
                        PlateNumber = "XYZ-789",
                        Lat = 36.2021,
                        Long = 37.1343,
                        FuelTankCapacity = 6000,
                        CargoTankCapacity = 3500,
                        FuelTankFullCapacity = 6000,
                        CargoTankFullCapacity = 3500,
                        FuelTankTypeId = 1,
                        CargoTankTypeId = 2
                    }
                };

                dataContext.Trucks.AddRange(trucks);
                dataContext.SaveChanges();
            }

            // bring ids
            var centerId = (dataContext.Centers.Where(c => c.Name == "مركز دمشق").FirstOrDefault()).Id;
            var statusId = (dataContext.Statuses.Where(s => s.Name == "نشط").FirstOrDefault()).Id;
            var shiftId = (dataContext.Shifts.Where(s => s.ShiftName == "Morning Shift").FirstOrDefault()).Id;
            var truckId = (dataContext.Trucks.Where(t => t.PlateNumber == "ABC-123").FirstOrDefault()).Id;
            // seeding users
            if (!dataContext.Users.Any())
            {
                // Create System Admin User
                var users = new List<User>
                {
                    new User
                    {
                        Name = "Rama",
                        Password = "123456",
                        Phone = "0987654321", // Example phone number
                        Email = "ramarehawe51@gmail.com",
                        Role = "SystemAdmin",
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        IsNotDeleted = true,
                        JwtToken = null // Will be assigned during authentication
                    },
                    new User
                    {
                        Name = "Admin User",
                        Email = "admin@fuelgo.sy",
                        Password = "admin123",
                        Phone = "0999999999",
                        Role = "Admin",
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        IsNotDeleted = true,
                        JwtToken = null // Will be assigned during authentication
                    },
                    new User
                    {
                        Name = "Driver User", 
                        Email = "driver@fuelgo.sy", 
                        Password = "driver123",
                        Phone = "0988888888", 
                        Role = "Driver", 
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now, 
                        IsNotDeleted = true,
                        JwtToken = null // Will be assigned during authentication
                    },
                    new User
                    {
                        Name = "Customer User", 
                        Email = "customer@fuelgo.sy", 
                        Password = "customer123",
                        Phone = "0977777777", 
                        Role = "Customer", 
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now, 
                        IsNotDeleted = true,JwtToken = null // Will be assigned during authentication
                    }
                };

                dataContext.Users.AddRange(users);
                dataContext.SaveChanges();


                // Create SystemAdmin entry
                var systemAdmin = new SystemAdmin
                {
                    UserId = dataContext.Users.FirstOrDefault(u => u.Email == "ramarehawe51@gmail.com").Id,
                    CreatedAt = DateTime.Now
                };

                dataContext.SystemAdmins.Add(systemAdmin);
                dataContext.SaveChanges();

                // Create Admin entry
                var admin = new Admin
                {
                    UserId = dataContext.Users.FirstOrDefault(u => u.Email == "admin@fuelgo.sy").Id,
                    CenterId = centerId,
                    StatusId = statusId
                };

                dataContext.Admins.Add(admin);
                dataContext.SaveChanges();

                // Create Driver entry
                var driver = new Driver
                {
                    UserId = dataContext.Users.FirstOrDefault(u => u.Email == "driver@fuelgo.sy").Id,
                    ShiftId = shiftId,
                    StatusId = statusId,
                    TruckId = truckId,
                    CenterId = centerId,
                    IsDriving = false
                };

                dataContext.Drivers.Add(driver);
                dataContext.SaveChanges();

                // Create Driver entry
                var customer = new Customer
                {
                    UserId = dataContext.Users.FirstOrDefault(u => u.Email == "customer@fuelgo.sy").Id
                };

                dataContext.Customers.Add(customer);
                dataContext.SaveChanges();
            }

            // Seeding GasStations
            if (!dataContext.GasStations.Any())
            {
                var gasStations = new List<GasStation>
                {
                    // دمشق – CityId = 1; using neighborhood "المزة" (first of Damascus neighborhoods)
                    new GasStation
                    {
                        NeighborhoodId = 1, // "المزة"
                        Name = "محطة شل دمشق",
                        Lat = 33.5138,
                        Long = 36.2765,
                        LocationDescription = "بالقرب من ساحة الأمويين في حي المزة"
                    },
                    // حلب – CityId = 2; using neighborhood "المشارقة" (first of Aleppo neighborhoods; note: neighborhoods for حلب start at ID 6)
                    new GasStation
                    {
                        NeighborhoodId = 6, // "المشارقة"
                        Name = "محطة شل حلب",
                        Lat = 36.2021,
                        Long = 37.1343,
                        LocationDescription = "في منطقة المشارقة بحلب"
                    },
                    // حمص – CityId = 3; using neighborhood "الوعر" (first of حمص neighborhoods; neighborhoods for حمص start at ID 11)
                    new GasStation
                    {
                        NeighborhoodId = 11, // "الوعر"
                        Name = "محطة بنزين حمص",
                        Lat = 34.7324,
                        Long = 36.7139,
                        LocationDescription = "في منطقة الوعر بحمص"
                    },
                    // حماة – CityId = 4; using neighborhood "الحاضر" (first of حماة neighborhoods; neighborhoods for حماة start at ID 16)
                    new GasStation
                    {
                        NeighborhoodId = 16, // "الحاضر"
                        Name = "محطة بنزين حماة",
                        Lat = 35.1379,
                        Long = 36.7518,
                        LocationDescription = "بالقرب من السوق المركزي في حي الحاضر"
                    },
                    // اللاذقية – CityId = 5; using neighborhood "مشروع الصليبة" (first of اللاذقية neighborhoods; neighborhoods for اللاذقية start at ID 21)
                    new GasStation
                    {
                        NeighborhoodId = 21, // "مشروع الصليبة"
                        Name = "محطة بنزين اللاذقية",
                        Lat = 35.5258,
                        Long = 35.7822,
                        LocationDescription = "في مشروع الصليبة باللاذقية"
                    },
                    // طرطوس – CityId = 6; using neighborhood "الكورنيش" (first of طرطوس neighborhoods; neighborhoods for طرطوس start at ID 26)
                    new GasStation
                    {
                        NeighborhoodId = 26, // "الكورنيش"
                        Name = "محطة بنزين طرطوس",
                        Lat = 34.8800,
                        Long = 35.8800,
                        LocationDescription = "على الكورنيش بطرطوس"
                    },
                    // درعا – CityId = 7; using neighborhood "درعا البلد" (first of درعا neighborhoods; neighborhoods for درعا start at ID 31)
                    new GasStation
                    {
                        NeighborhoodId = 31, // "درعا البلد"
                        Name = "محطة بنزين درعا",
                        Lat = 32.6200,
                        Long = 36.0900,
                        LocationDescription = "في قلب درعا البلد"
                    },
                    // السويداء – CityId = 8; using neighborhood "شهبا" (first of السويداء neighborhoods; neighborhoods for السويداء start at ID 36)
                    new GasStation
                    {
                        NeighborhoodId = 36, // "شهبا"
                        Name = "محطة بنزين السويداء",
                        Lat = 32.7090,
                        Long = 36.5880,
                        LocationDescription = "في منطقة شهبا بالسويداء"
                    },
                    // الحسكة – CityId = 9; using neighborhood "القامشلي" (first of الحسكة neighborhoods; neighborhoods for الحسكة start at ID 41)
                    new GasStation
                    {
                        NeighborhoodId = 41, // "القامشلي"
                        Name = "محطة بنزين الحسكة",
                        Lat = 36.5000,
                        Long = 40.7500,
                        LocationDescription = "في منطقة القامشلي بالحسكة"
                    },
                    // دير الزور – CityId = 10; using neighborhood "البوكمال" (first of دير الزور neighborhoods; neighborhoods for دير الزور start at ID 46)
                    new GasStation
                    {
                        NeighborhoodId = 46, // "البوكمال"
                        Name = "محطة بنزين دير الزور",
                        Lat = 35.3300,
                        Long = 40.1300,
                        LocationDescription = "بالقرب من البوكمال في دير الزور"
                    },
                    // الرقة – CityId = 11; using neighborhood "الدرعية" (first of الرقة neighborhoods; neighborhoods for الرقة start at ID 51)
                    new GasStation
                    {
                        NeighborhoodId = 51, // "الدرعية"
                        Name = "محطة بنزين الرقة",
                        Lat = 35.9500,
                        Long = 39.0170,
                        LocationDescription = "في حي الدرعية بالرقة"
                    },
                    // إدلب – CityId = 12; using neighborhood "جسر الشغور" (first of إدلب neighborhoods; neighborhoods for إدلب start at ID 56)
                    new GasStation
                    {
                        NeighborhoodId = 56, // "جسر الشغور"
                        Name = "محطة بنزين إدلب",
                        Lat = 35.9500,
                        Long = 36.6660,
                        LocationDescription = "بالقرب من جسر الشغور بإدلب"
                    }
                };

                dataContext.GasStations.AddRange(gasStations);
                dataContext.SaveChanges();
            }

            // Seeding FuelDetails
            if (!dataContext.FuelDetails.Any())
            {
                var fuelDetails = new List<FuelDetail>
                {
                    new FuelDetail
                    {
                        FuelTypeId = 1,  // e.g., بنزين
                        CenterId = 1,    // a valid center id
                        Price = 12000     // example price
                    },
                    new FuelDetail
                    {
                        FuelTypeId = 2,  // e.g., مازوت
                        CenterId = 2,
                        Price = 12000
                    }
                };

                dataContext.FuelDetails.AddRange(fuelDetails);
                dataContext.SaveChanges();
            }

            // Seeding ConstantDictionary
            if (!dataContext.ConstantDictionaries.Any())
            {
                var constants = new List<ConstantDictionary>
                {
                    new ConstantDictionary { Key = "Distance", Value = 1.5 },
                    new ConstantDictionary { Key = "Min", Value = 0.5 },
                    new ConstantDictionary { Key = "Max", Value = 5.0 },
                    // Add more key/value pairs as needed
                };

                dataContext.ConstantDictionaries.AddRange(constants);
                dataContext.SaveChanges();
            }
            var customerUser = dataContext.Users.FirstOrDefault(u => u.Email == "customer@fuelgo.sy");
            var customerRecord = dataContext.Customers.FirstOrDefault(c => c.UserId == customerUser.Id);

            // Seeding CustomerCars (with soft delete flag)
            if (!dataContext.CustomerCars.Any())
            {
                var customerCars = new List<CustomerCar>
                {
                    new CustomerCar
                    {
                        CustomerId = customerRecord.Id, 
                        PlateNumber = "CAR-001",
                        Brand = "Toyota",
                        Model = "Corolla",
                        Color = "White",
                        Phone = "0999000111",
                        IsDeleted = false
                    }
                };

                dataContext.CustomerCars.AddRange(customerCars);
                dataContext.SaveChanges();
            }

            // Seeding CustomerApartments (with soft delete flag)
            if (!dataContext.CustomerApartments.Any())
            {
                var customerApartments = new List<CustomerApartment>
                {
                    new CustomerApartment
                    {
                        CustomerId = customerRecord.Id,
                        NeighborhoodId = 1,
                        Name = "شقة المستخدم",
                        Phone = "0999000222",
                        Lat = 33.5140,
                        Long = 36.2770,
                        LocationDescription = "قرب محطة المترو",
                        IsDeleted = false
                    }
                };

                dataContext.CustomerApartments.AddRange(customerApartments);
                dataContext.SaveChanges();
            }

            // Seeding Wallets
            if (!dataContext.Wallets.Any())
            {
                var wallets = new List<Wallet>
                {
                    new Wallet
                    {
                        UserId = customerRecord.Id,
                        Amount = 100.0
                    }
                };

                dataContext.Wallets.AddRange(wallets);
                dataContext.SaveChanges();
            }

            // Seeding WalletTransactions
            if (!dataContext.WalletTransactions.Any())
            {
                var walletTransactions = new List<WalletTransaction>
                {
                    new WalletTransaction
                    {
                        WalletId = dataContext.Wallets.First().Id,  // assuming at least one wallet exists
                        MadeBy = customerRecord.Id,  // user id making the transaction
                        Amount = 50.0   // positive for deposit, negative for withdrawal
                    },
                    new WalletTransaction
                    {
                        WalletId = dataContext.Wallets.First().Id,
                        MadeBy = customerRecord.Id,
                        Amount = -20.0
                    }
                };

                dataContext.WalletTransactions.AddRange(walletTransactions);
                dataContext.SaveChanges();
            }

            // Retrieve required related records
            var fuelType = dataContext.FuelTypes.FirstOrDefault(f => f.Name == "بنزين");

            // Retrieve the pending order status ("قيد الانتظار") from order statuses
            var pendingOrderStatus = dataContext.Statuses
                .FirstOrDefault(s => s.Name == "قيد الانتظار" && s.StatusTypeId == orderType.Id);

            // Retrieve an existing customer apartment record (seeded earlier)
            var customerApartment = dataContext.CustomerApartments.FirstOrDefault();

            // Seeding Orders
            if (!dataContext.Orders.Any())
            {
                var orders = new List<Order>
                {
                    new Order
                    {
                        Date = DateTime.UtcNow,
                        OrderNumber = "ORD-1001",
                        Lat = 33.5138,
                        Long = 36.2765,
                        LocationDescription = "شارع الحرية",
                        NeighborhoodId = 1, // Assumes neighborhood with ID=1 exists (e.g., "المزة" in دمشق)
                        FuelTypeId = fuelType != null ? fuelType.Id : 1, // Default to 1 if not found
                        OrderedQuantity = 100.0,
                        FinalQuantity = null,
                        FinalPrice = null,
                        IsItUrgent = false,
                        CustomerCarId = null,              // Leave null if no car is selected
                        CustomerApartmentId = customerApartment != null ? customerApartment.Id : (int?)null,
                        CustomerId = customerRecord != null ? customerRecord.Id : 1,
                        DriverId = null,                   // No driver assigned yet
                        StatusId = pendingOrderStatus != null ? pendingOrderStatus.Id : 1, // Default to 1 if not found
                        IsActive = true,
                        AuthCode = "AUTH1234"
                    }
                };

                dataContext.Orders.AddRange(orders);
                dataContext.SaveChanges();
            }


            // Retrieve required entities for TruckTankRefills and TruckHandovers
            var truckForRefill = dataContext.Trucks.FirstOrDefault(t => t.PlateNumber == "ABC-123");
            var gasStation = dataContext.GasStations.FirstOrDefault(); // Gets the first seeded gas station
            var driverUser = dataContext.Users.FirstOrDefault(u => u.Email == "driver@fuelgo.sy");
            var driverRecord = dataContext.Drivers.FirstOrDefault(d => d.UserId == driverUser.Id);
            var adminUser = dataContext.Users.FirstOrDefault(u => u.Email == "admin@fuelgo.sy");
            var adminRecord = dataContext.Admins.FirstOrDefault(a => a.UserId == adminUser.Id);

            // Seeding TruckTankRefills
            if (!dataContext.TruckTankRefills.Any() && truckForRefill != null && gasStation != null && driverRecord != null)
            {
                var truckTankRefills = new List<TruckTankRefill>
                {
                    new TruckTankRefill
                    {
                        TruckId = truckForRefill.Id,
                        QuantityCargoRefill = 500.0,
                        QuantityFuelRefill = 300.0,
                        Price = 150.0,
                        GasStationId = gasStation.Id,
                        DriverId = driverRecord.Id
                    }
                };

                dataContext.TruckTankRefills.AddRange(truckTankRefills);
                dataContext.SaveChanges();
            }

            // Seeding TruckHandovers
            if (!dataContext.TruckHandovers.Any() && truckForRefill != null && driverRecord != null && adminRecord != null)
            {
                var truckHandovers = new List<TruckHandover>
                {
                    new TruckHandover
                    {
                        TruckId = truckForRefill.Id,
                        CargoQuantity = 250.0,
                        FuelQuantity = 150.0,
                        CargoVarience = 5.0,
                        FuelVarience = 3.0,
                        Money = 100.0,
                        DriverId = driverRecord.Id,
                        AdminId = adminRecord.Id
                    }
                };

                dataContext.TruckHandovers.AddRange(truckHandovers);
                dataContext.SaveChanges();
            }

        }


    }
}
