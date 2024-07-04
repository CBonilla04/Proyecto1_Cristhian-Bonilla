using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Proyecto1_CristhianBonilla.Models;    

namespace Proyecto1_CristhianBonilla.Utils
{
    public class AppDbContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Scales> Scales { get; set; }
        public DbSet<Flights> Flights { get; set; }
        public DbSet<PassengerType> PassengerType { get; set; }
        public DbSet<Reservations> Reservations { get; set; }
        public DbSet<FlightPassengers> FlightPassengers { get; set; }
        public DbSet<FlightScales> FlightScales { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            try
            {
                var dbCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (dbCreator != null)
                {
                    if (!dbCreator.CanConnect())
                    {
                        dbCreator.Create();
                    }

                    if (!dbCreator.HasTables())
                    {
                        dbCreator.CreateTables();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }   
            
        }
    }
}
