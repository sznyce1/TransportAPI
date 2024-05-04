using Microsoft.AspNetCore.Mvc;
using TransportAPI.Models;
using TransportAPI.Services;

namespace TransportAPI.Controllers
{
    [Route("api/driver")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        private readonly IDriverService _driverService;

        public DriverController(IDriverService driverService)
        {
            _driverService = driverService;
        }
        [HttpGet]
        public ActionResult<IEnumerable<DriverDto>> GetAll()
        {
            var drivers = _driverService.GetAll();
            return Ok(drivers);
        }
        [HttpGet("{id}")]
        public ActionResult<DriverDto> GetById([FromRoute]int id)
        {
            var driver = _driverService.GetById(id);
            return Ok(driver);
        }
        [HttpPost]
        public ActionResult CreateDriver([FromBody] CreateDriverDto dto)
        {
            var id = _driverService.Create(dto);
            return Created($"/api/driver/{id}", null);
        }
        [HttpPut("{id}")]
        public ActionResult Update([FromBody] UpdateDriverDto dto, [FromRoute] int id)
        {
            _driverService.Update(id, dto);

            return Ok();

        }
        
    }
}
