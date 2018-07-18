using AirportRESRfulApi.BLL.Interfaces;
using AirportRESRfulApi.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace AirportRESRfulApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private IFlightsService _flightsSrvice;
        public FlightsController(IFlightsService flightsSrvice)
        {
            _flightsSrvice = flightsSrvice;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _flightsSrvice.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var entity = await _flightsSrvice.GetAsync(id);

            if (entity == null) return NotFound();

            return Ok(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FlightDto entity)
        {
            var result = await _flightsSrvice.AddAsync(entity);

            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] FlightDto entity)
        {
            var result = await _flightsSrvice.UpdateAsync(entity, id);

            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromBody] FlightDto entity)
        {
            var result = await _flightsSrvice.DeleteAsync(entity);

            return Ok();
        }
    }
}
