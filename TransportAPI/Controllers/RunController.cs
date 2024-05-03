using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransportAPI.Entities;
using TransportAPI.Models;
using TransportAPI.Services;

namespace TransportAPI.Controllers
{
    [Route("api/run")]
    public class RunController : ControllerBase
    {
        private readonly TransportDbContext _dbcontext;
        private readonly IMapper _mapper;
        private readonly IRunService _runService;

        public RunController(IRunService runService)
        {
            _runService = runService;
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromBody] UpdateRunDto dto, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updated = _runService.Update(id, dto);
            if (updated)
            {
                return Ok();
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute]int id)
        {
            bool isDeleted = _runService.Delete(id);
            if (isDeleted)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public ActionResult CreateRun([FromBody] CreateRunDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id = _runService.Create(dto);

            return Created($"/api/run/{id}",null);
        }

        [HttpGet]
        public ActionResult<IEnumerable<RunDto>> GetAll()
        {
            var runsDtos = _runService.GetAll();
            return Ok(runsDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<RunDto> Get([FromRoute]int id)
        {
            var run = _runService.GetById(id);
            if (run is null) 
            {
                return NotFound();
                
            }
            return Ok(run);
        }

    }
}
