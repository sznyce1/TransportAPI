using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        public void Update([FromRoute] int id, [FromBody] UpdateDriverDto dto);
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
            ValidateLicence(driver.DrivingCategories);
            _dbContext.Add(driver);
            _dbContext.SaveChanges();
            return driver.Id;
        }
        public void Update([FromRoute] int id, [FromBody] UpdateDriverDto dto)
        {
            var driver = _dbContext
                .Drivers
                .FirstOrDefault(r => r.Id == id);
            if (driver == null)
            {
                throw new NotFoundException("Driver not found");
            }
            ValidateLicence(driver.DrivingCategories);
            driver.Name = dto.Name;
            driver.SecondName = dto.SecondName;
            driver.DrivingCategories = dto.DrivingCategories;
            if (dto.Runs != null)
            {
                //driver.Runs.RemoveRange(0, dto.Runs.Count);
                driver.Runs.Add(_mapper.Map<Run>(dto.Runs));
            }            
        }
        private void ValidateLicence(string licence)
        {
            licence = licence.ToUpper();
            foreach (char c in licence)
            {
                if (c != 'A' && c != 'B' && c != 'C' && c != 'D')
                {
                    throw new InvalidDrivingLicenceException();
                }
            }
        }

    }
}

    

