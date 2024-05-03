using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TransportAPI.Entities;
using TransportAPI.Models;

namespace TransportAPI.Services
{
    public interface IRunService
    {
        int Create(CreateRunDto dto);
        IEnumerable<RunDto> GetAll();
        RunDto GetById(int id);
        bool Delete(int id);
        public bool Update(int id, UpdateRunDto dto);
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
                return null;
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
            var run = _mapper.Map<Run>(dto);
            _dbContext.Add(run);
            _dbContext.SaveChanges();

            return run.Id;
        }
        public bool Delete(int id)
        {
            var run = _dbContext
                .Runs
                .FirstOrDefault(r => r.Id == id);
            if(run is null)
            {
                return false;
            }
            _dbContext.Runs.Remove(run);
            _dbContext.SaveChanges(); 
            return true;
        }
        public bool Update(int id, UpdateRunDto dto)
        {
            var run = _dbContext
                .Runs
                .FirstOrDefault(r => r.Id == id);
            if (run is null)
            {
                return false;
            }
            run.CarId = dto.CarId;
            run.DriverId = dto.DriverId;
            run.StartDate = dto.StartDate;
            run.EndDate = dto.EndDate;
            run.Distance = dto.Distance;
            run.AverageFuelConsumption = dto.AverageFuelConsumption;

            _dbContext .SaveChanges();
            return true;
        }
    }
}
