using Microsoft.Data.SqlClient;
using TransportAPI.Entities;

namespace TransportAPI
{
    public class TransportSeeder
    {
        private readonly TransportDbContext _dbContext;
        public TransportSeeder(TransportDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Runs.Any())
                {
                    var Runs = GetRuns();
                    _dbContext.Runs.AddRange(Runs);
                    _dbContext.SaveChanges();
                }
            }
            else
            {
                throw new Exception("Databse error");
            }
        }
        private IEnumerable<Run> GetRuns()
        {
            Driver driver = GetDriver();
            Car car = GetCar();
            var runs = new List<Run>()
            {
                new Run()
                {
                    StartDate = DateTime.Now,
                    EndDate = null,
                    Distance = 50,
                    AverageFuelConsumption = 7.7,
                    Driver = driver,
                    Car = car
                },
                new Run()
                {
                    StartDate = DateTime.Now.AddDays(-7),
                    EndDate = DateTime.Now,
                    Distance = 1250,
                    AverageFuelConsumption = 9.7,
                    Driver = driver,
                    Car = car
                }
            };
            return runs;
        }
        private Driver GetDriver()
        {
            return new Driver()
            {
                Name = "TestName",
                SecondName = "TestSecondName",
                DrivingCategories = "AB"
            };
        }
        private Car GetCar()
        {
            return new Car()
            {
                Model = "TestModel",
                RegistrationNumber = "SK-111555",
                CarType = "Motorcycle"
            };
        }
    }
}
