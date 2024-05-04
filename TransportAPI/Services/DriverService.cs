using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TransportAPI.Entities;

namespace TransportAPI.Services
{
    public interface IDriverService
    {
        public IEnumerable<Driver> GetAll();
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
        public IEnumerable<Driver> GetAll()
        {
            var drivers = _dbContext.Drivers.Include(r => r.Runs).ToList();
            return drivers;
        }
    }

    
}
