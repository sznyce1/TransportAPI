using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransportAPI.Entities;
using TransportAPI.Models;

namespace TransportAPI.Controllers
{
    [Route("api/run")]
    public class RunController : ControllerBase
    {
        private readonly TransportDbContext _dbcontext;
        private readonly IMapper _mapper;
        public RunController(TransportDbContext dbContext, IMapper mapper)
        {
            _dbcontext = dbContext;
            _mapper = mapper;
        }
        [HttpPost]
        public ActionResult CreateRun([FromBody] CreateRunDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var run = _mapper.Map<Run>(dto);
            _dbcontext.Add(run);
            _dbcontext.SaveChanges();

            return Created($"/api/run/{run.Id}",null);
        }
        [HttpGet]
        public ActionResult<IEnumerable<RunDto>> GetAll()
        {
            var runs = _dbcontext
                .Runs
                .Include(r => r.Car)
                .Include(r => r.Driver)
                .ToList();
            var rundsDtos = _mapper.Map<List<RunDto>>(runs);
            return Ok(rundsDtos);
        }
        [HttpGet("{id}")]
        public ActionResult<RunDto> Get([FromRoute]int id)
        {
            var run = _dbcontext
                .Runs
                .Include(r => r.Car)
                .Include(r => r.Driver)
                .FirstOrDefault(r => r.Id == id);

            if (run is not null) 
            {
                var runDto = _mapper.Map<RunDto>(run);
                return Ok(runDto);
            }
            else
            {
                return NotFound();
            }
            
        }
    }
}
