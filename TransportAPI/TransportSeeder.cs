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
                    var runs = GetRuns();
                    _dbContext.Runs.AddRange(runs);
                    var drivers = GetDrivers();
                    _dbContext.Drivers.AddRange(drivers);
                    var cars = GetCars();
                    _dbContext.Cars.AddRange(cars);
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
            Driver driver = GetDriver("AB");
            Car car = GetCar("Motorcycle");
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
        private IEnumerable<Driver> GetDrivers()
        {
            var drivers = new List<Driver>()
            {
                GetDriver("A"),
                GetDriver("B"),
                GetDriver("C"),
                GetDriver("D"),
                GetDriver("BC"),
                GetDriver("BD")
            };
            return drivers;
        }
        private IEnumerable<Car> GetCars()
        {
            var cars = new List<Car>()
            {
                GetCar("motorcycle"),
                GetCar("passenger car"),
                GetCar("truck"),
                GetCar("bus")

            };
            return cars;
        }
        private Driver GetDriver(string drivingCategories)
        {
            return new Driver()
            {
                Name = "TestName",
                SecondName = "TestSecondName",
                DrivingCategories = drivingCategories
            };
        }
        private Car GetCar(string carType)
        {
            return new Car()
            {
                Model = "TestModel",
                RegistrationNumber = "SK-111555",
                CarType = carType
            };
        }
    }
}
