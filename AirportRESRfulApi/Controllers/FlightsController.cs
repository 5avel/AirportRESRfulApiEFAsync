using AirportRESRfulApi.BLL.Interfaces;
using AirportRESRfulApi.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

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
        public IActionResult Get()
        {
            return Ok(_flightsSrvice.Get());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var ticket = _flightsSrvice.GetById(id);

            if (ticket == null) return NotFound();

            return Ok(ticket);
        }

        [HttpPost]
        public IActionResult Post([FromBody] FlightDto entity)
        {
            _flightsSrvice.Make(entity);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] FlightDto ticket)
        {
            _flightsSrvice.Update(ticket);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok();
        }
    }
}
