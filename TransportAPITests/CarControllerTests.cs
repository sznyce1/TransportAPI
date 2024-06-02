using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Testing;
using TransportAPI;

namespace TransportAPITests
{
    public class CarControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        public CarControllerTests(WebApplicationFactory<Program> factory)
        {

            _client = factory.CreateClient();

        }

        [Fact]
        public async Task GetAll_Given_ReturnsAllCars()
        {
            //arrange
            //act
            var response =await _client.GetAsync("api/car");
            //assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
    }
}
