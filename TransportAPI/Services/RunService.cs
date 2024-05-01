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
    }
}
