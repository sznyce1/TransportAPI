using Microsoft.AspNetCore.Mvc;
using TransportAPI.Models;
using TransportAPI.Services;

namespace TransportAPI.Controllers
{
    [Route("api/car")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var cars = _carService.GetAll();
            return Ok(cars);
        }

        [HttpGet("{id}")]
        public ActionResult Get([FromRoute]int id)
        {
            var car = _carService.GetById(id);
            return Ok(car);
        }
        [HttpPost]
        public ActionResult Create([FromBody ] CreateCarDto dto)
        {
            var id = _carService.Create(dto);
            return Created($"api/car/{id}", null);
        }


    }
}
