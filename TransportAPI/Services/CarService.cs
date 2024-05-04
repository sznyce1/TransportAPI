﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TransportAPI.Entities;
using TransportAPI.Exceptions;
using TransportAPI.Models;

namespace TransportAPI.Services
{
    public interface ICarService
    {
        IEnumerable<CarDto> GetAll();
        public CarDto GetById(int id);
        public int Create(CreateCarDto dto);
        public void Update(int id, UpdateCarDto dto);
    }

    public class CarService : ICarService
    {
        private readonly TransportDbContext _dbContext;
        private readonly IMapper _mapper;

        public CarService(TransportDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IEnumerable<CarDto> GetAll()
        {
            var cars = _dbContext
                .Cars
                .Include(r => r.Runs)
                .ToList();
            var result = _mapper.Map<List<CarDto>>(cars);
            return result;
        }

        public CarDto GetById(int id)
        {
            var car = _dbContext
                .Cars
                .Include(r => r.Runs)
                .FirstOrDefault(r => r.Id == id);
            var result = _mapper.Map<CarDto>(car);
            return result;
        }
        public int Create(CreateCarDto dto)
        {
            ValidateCarType(dto.CarType);
            var car = _mapper.Map<Car>(dto);
            _dbContext.Add(car);
            _dbContext.SaveChanges();
            return car.Id;
        }
        public void Update(int id, UpdateCarDto dto)
        {
            ValidateCarType(dto.CarType);
            var car = _dbContext
                .Cars
                .FirstOrDefault(r => r.Id == id);
            if (car == null)
            {
                throw new NotFoundException("Car not found");
            }
            car.Model = dto.Model;
            car.RegistrationNumber = dto.RegistrationNumber;
            car.CarType = dto.CarType;
            _dbContext.SaveChanges();
        }
        private void ValidateCarType(string type)
        {
            type = type.ToLower();
            if(!(type.Contains("motorcycle") || type.Contains("passenger car") || type.Contains("truck") || type.Contains("bus")))
            {
                throw new InvalidArgumentException("Invalid car type");
            }
        }

    }
}