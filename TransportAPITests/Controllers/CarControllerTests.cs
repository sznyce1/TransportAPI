using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using System.Net;
using TransportAPI;
using TransportAPI.Entities;
using Microsoft.Extensions.DependencyInjection;
using Castle.Components.DictionaryAdapter.Xml;
using TransportAPI.Models;
using Newtonsoft.Json;
using System.Text;

namespace TransportAPITests.Controllers
{
    public class CarControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Program> _factory;
        public CarControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var dbContextOptions = services.SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<TransportDbContext>));
                        services.Remove(dbContextOptions);
                        services.AddDbContext<TransportDbContext>(options => options.UseInMemoryDatabase("TransportDb"));
                    });
                });
            _client = _factory.CreateClient();

        }

        [Fact]
        public async Task GetAll_ReturnsAllCars()
        {
            //arrange
            //act
            var response = await _client.GetAsync("api/car");
            //assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetById_GivenExistingId_ReturnsOK()
        {
            //arrange
            var car = SeedSampleCar();
            //act
            var response = await _client.GetAsync($"api/car/{car.Id}");
            //assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Fact]
        public async Task Delete_ForNonExistingCar_ReturnsRestaurantNotFound()
        {
            //arrange
            Car car = SeedSampleCar();
            RemoveCar(car);

            var response = await _client.DeleteAsync($"api/car/{car.Id}");
            //assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Delete_ForExistingRestaurant_ReturnsNoContent()
        {
            //arrange
            var car = SeedSampleCar();
            //act
            var response = await _client.DeleteAsync($"api/car/{car.Id}");

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task CreateCar_WothValidComponents_ReturnsCreated()
        {
            var model = new CreateCarDto()
            {
                CarType = "motorcycle",
                Model = "testmodel",
                RegistrationNumber = "sz-12345",
                Runs = new List<RunDto> { }
                
            };

            var json = JsonConvert.SerializeObject(model);
            var httpContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/car/", httpContent);

            response.StatusCode.Should().Be(HttpStatusCode.Created);
            //response.Headers.Should().NotBeNull();
        }

        private Car SeedSampleCar()
        {
            var car = new Car()
            {
                CarType = "testtype",
                Model = "testmodel",
                RegistrationNumber = "sz-123456"
            };
            //seed
            var dbContext = GetDbContext();

            dbContext.Add(car);
            dbContext.SaveChanges();

            return car;
        }

        private void RemoveCar(Car car)
        {
            var dbContext = GetDbContext();
            dbContext.Remove(car);
            dbContext.SaveChanges();
        }
        private TransportDbContext GetDbContext()
        {
            var scope = _factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<TransportDbContext>();
            return dbContext;
        }
    }
}
