using AutoMapper;
using FuelGo.Data;
using FuelGo.Inerfaces;
using FuelGo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace FuelGo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly DataContext _context;
        public TestController(UserInfoService userInfoService, IUnitOfWork unitOfWork, IConfiguration configuration,
            DataContext context) : 
            base(userInfoService, unitOfWork)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpGet("health-check")]
        public IActionResult HealthCheck()
        {
            return Ok("I am alive");
        }
        [HttpGet("test-error")]
        public IActionResult TestError()
        {
            int x = 0;
            return Ok(1 / x);
        }
        [HttpGet("test-database")]
        public IActionResult TestDataBase()
        {
            var x = _unitOfWork._adminRepository.GetShifts();

            return Ok(x);
        }

        [HttpPost("reset-database")]
        public IActionResult ResetDatabase()
        {
            if (!Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")!.Equals("Development"))
            {
                return Forbid("This action is only allowed in development.");
            }

            try
            {
                // Step 1: Drop and recreate the DB using SQL
                var baseConnectionString = _configuration.GetConnectionString("DefaultConnection");
                var builder = new SqlConnectionStringBuilder(baseConnectionString)
                {
                    InitialCatalog = "master"
                };

                using var connection = new SqlConnection(builder.ConnectionString);
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"
                    ALTER DATABASE FuelGo SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                    DROP DATABASE FuelGo;
                    CREATE DATABASE FuelGo;
                    ";
                command.ExecuteNonQuery();

                // Step 2: Apply Migrations
                _context.Database.Migrate(); // = Update-Database

                // Step 3: Seed Data
                var seeder = new Seed(_context); // _context is your DataContext or FuelGoContext
                seeder.SeedDataContext(); // Explicit call

                return Ok("Database reset, migrations applied, and seed data inserted.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Operation failed: {ex.Message}");
            }
        }




    }
}
