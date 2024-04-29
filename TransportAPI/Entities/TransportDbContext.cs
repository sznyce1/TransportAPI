using Microsoft.EntityFrameworkCore;

namespace TransportAPI.Entities
{
    public class TransportDbContext : DbContext
    {
        private string _connectionString = "Server=(localdb)\\mssqllocaldb;Database=TransportDb;Trusted_Connection=True;";
        public DbSet<Run> Runs { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Driver> Drivers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>().Property(r => r.Model).IsRequired().HasMaxLength(20);
            modelBuilder.Entity<Car>().Property(r => r.RegistrationNumber).IsRequired().HasMaxLength(9);
            modelBuilder.Entity<Car>().Property(r => r.CarType).IsRequired().HasMaxLength(20);

            modelBuilder.Entity<Driver>().Property(r => r.Name).IsRequired().HasMaxLength(20);
            modelBuilder.Entity<Driver>().Property(r => r.SecondName).IsRequired().HasMaxLength(40);
            modelBuilder.Entity<Driver>().Property(r => r.DrivingCategories).IsRequired().HasMaxLength(4);

            modelBuilder.Entity<Run>().Property(r => r.Distance).IsRequired().HasMaxLength(5);
            modelBuilder.Entity<Run>().Property(r => r.AverageFuelConsumption).HasMaxLength(5);
            modelBuilder.Entity<Run>().Property(r => r.StartDate).IsRequired();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
