using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TransportAPI.Entities;
using TransportAPI.Exceptions;
using TransportAPI.Models;

namespace TransportAPI.Services
{
    public interface IDriverService
    {
        public IEnumerable<DriverDto> GetAll();
        public DriverDto GetById(int id);
        public int Create(CreateDriverDto dto);
    }
    public class DriverService : IDriverService
    {
        private readonly TransportDbContext _dbContext;
        private readonly IMapper _mapper;
        public DriverService(TransportDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public IEnumerable<DriverDto> GetAll()
        {
            var drivers = _dbContext
                .Drivers
                .Include(r => r.Runs)
                .ToList();
            var results = _mapper.Map<List<DriverDto>>(drivers);
            return results;
        }
        public DriverDto GetById(int id)
        {
            var driver = GetDriver(id);
            var result = _mapper.Map<DriverDto>(driver);
            return result;
        }
        private Driver GetDriver(int id)
        {
            var driver = _dbContext
                .Drivers
                .Include(r => r.Runs)
                .FirstOrDefault(r => r.Id == id);
            if (driver == null)
            {
                throw new NotFoundException("Driver not found");
            }
            return driver;
        }
        public int Create(CreateDriverDto dto)
        {
            var driver = _mapper.Map<Driver>(dto);
            _dbContext.Add(driver);
            _dbContext.SaveChanges();
            return driver.Id;
        }
    }

    
}
