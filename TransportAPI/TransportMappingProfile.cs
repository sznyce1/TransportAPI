using AutoMapper;
using TransportAPI.Entities;
using TransportAPI.Models;

namespace TransportAPI
{
    public class TransportMappingProfile : Profile
    {
        public TransportMappingProfile()
        {
            CreateMap<Run, RunDto>()
                .ForMember(m => m.Name, c => c.MapFrom(s => s.Driver.Name))
                .ForMember(m => m.SecondName, c => c.MapFrom(s => s.Driver.SecondName))
                .ForMember(m => m.Model, c => c.MapFrom(s => s.Car.Model))
                .ForMember(m => m.RegistrationNumber, c => c.MapFrom(s => s.Car.RegistrationNumber));
            CreateMap<Car, CarDto>();
            CreateMap<Driver,DriverDto>();
        }
    }
}
