using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TransportAPI.Entities;
using TransportAPI.Exceptions;
using TransportAPI.Models;

namespace TransportAPI.Services
{
    public interface IRunService
    {
        int Create(CreateRunDto dto);
        IEnumerable<RunDto> GetAll();
        RunDto GetById(int id);
        void Delete(int id);
        void Update(int id, UpdateRunDto dto);
    }

    public class RunService : IRunService
    {
        private readonly TransportDbContext _dbContext;
        private readonly IMapper _mapper;

        public RunService(TransportDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public RunDto GetById(int id)
        {
            var run = _dbContext
                .Runs
                .Include(r => r.Car)
                .Include(r => r.Driver)
                .FirstOrDefault(r => r.Id == id);
            if (run is not null)
            {
                var result = _mapper.Map<RunDto>(run);
                return result;
            }
            else
            {
                throw new NotFoundException("Run not found");
            }
        }

        public IEnumerable<RunDto> GetAll()
        {
            var runs = _dbContext
                .Runs
                .Include(r => r.Car)
                .Include(r => r.Driver)
                .ToList();
            var rundsDtos = _mapper.Map<List<RunDto>>(runs);
            return rundsDtos;
        }

        public int Create(CreateRunDto dto)
        {
            var car = _dbContext
                .Cars
                .FirstOrDefault(r => r.Id == dto.CarId);
            if (car == null)
            {
                throw new NotFoundException("Car not found");
            }
            var driver = _dbContext
                .Drivers
                .FirstOrDefault(r => r.Id == dto.DriverId);
            if (driver == null)
            {
                throw new NotFoundException("Driver not found");
            }
                
            var run = _mapper.Map<Run>(dto);

            ValidateRun(driver.Id, car.Id);

            _dbContext.Add(run);
            _dbContext.SaveChanges();

            return run.Id;
        }

        public void Delete(int id)
        {
            var run = _dbContext
                .Runs
                .FirstOrDefault(r => r.Id == id);
            if(run is null)
            {
                throw new NotFoundException("Run not found");
            }
            _dbContext.Runs.Remove(run);
            _dbContext.SaveChanges(); 

        }

        public void Update(int id, UpdateRunDto dto)
        {
            var run = _dbContext
                .Runs
                .FirstOrDefault(r => r.Id == id);
            if (run is null)
            {
                throw new NotFoundException("Run not found");
            }
            var car = _dbContext
                .Cars
                .FirstOrDefault(r => r.Id == dto.CarId);
            if (car == null)
            {
                throw new NotFoundException("Car not found");
            }
            var driver = _dbContext
                .Drivers
                .FirstOrDefault(r => r.Id == dto.DriverId);
            if (driver == null)
            {
                throw new NotFoundException("Driver not found");
            }

            ValidateRun(driver.Id,car.Id);

            run.CarId = dto.CarId;
            run.DriverId = dto.DriverId;
            run.StartDate = dto.StartDate;
            run.EndDate = dto.EndDate;
            run.Distance = dto.Distance;
            run.AverageFuelConsumption = dto.AverageFuelConsumption;

            _dbContext .SaveChanges();
        }

        private void ValidateRun(int driverId, int carId)
        {
            var driver = _dbContext
                .Drivers
                .FirstOrDefault(r => r.Id == driverId);
            var car = _dbContext
                .Cars
                .FirstOrDefault(r => r.Id == carId);
            if(car == null || driver == null)
            {
                throw new NotFoundException("Driver or car not found");
            }
            var type = car.CarType.ToLower();
            var licence = driver.DrivingCategories.ToLower();
            if (type.Contains("motorcycle"))
            {
                if (!licence.Contains('a'))
                {
                    throw new InvalidArgumentException("invalid driving licence for this type of vehicle");
                }
            }
            else if (type.Contains("passenger car"))
            {

                if (!licence.Contains('b'))
                {
                    throw new InvalidArgumentException("invalid driving licence for this type of vehicle");
                }
            }
            else if (type.Contains("truck"))
            {
                if (!licence.Contains('c'))
                {
                    throw new InvalidArgumentException("invalid driving licence for this type of vehicle");
                }
            }
            else if (type.Contains("bus"))
            {
                if (!licence.Contains('d'))
                {
                    throw new InvalidArgumentException("invalid driving licence for this type of vehicle");
                }
            }
        }
    }
}
