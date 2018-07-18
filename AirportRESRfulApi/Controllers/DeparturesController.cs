using AirportRESRfulApi.BLL.Interfaces;
using AirportRESRfulApi.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AirportRESRfulApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeparturesController : ControllerBase
    {
        private IDeparturesService _departuresSrvice;
        public DeparturesController(IDeparturesService departuresSrvice)
        {
            _departuresSrvice = departuresSrvice;
        }

        // GET api/Flights
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_departuresSrvice.Get());
        }

        // GET api/Flights/2
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var ticket = _departuresSrvice.GetById(id);

            if (ticket == null) return NotFound();

            return Ok(ticket);
        }

        // POST api/Flights
        [HttpPost]
        public IActionResult Post([FromBody] DepartureDto entity)
        {
            _departuresSrvice.Make(entity);
            return Ok();
        }

        // PUT api/Flights/2
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] DepartureDto entity)
        {
            _departuresSrvice.Update(entity);
            return Ok();
        }

        // DELETE api/Flights/2
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok();
        }
    }
}
