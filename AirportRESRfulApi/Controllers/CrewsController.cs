using AirportRESRfulApi.BLL.Interfaces;
using AirportRESRfulApi.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AirportRESRfulApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrewsController : ControllerBase
    {
        private ICrewsService _crewsSrvice;
        public CrewsController(ICrewsService crewsSrvice)
        {
            _crewsSrvice = crewsSrvice;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_crewsSrvice.Get());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var ticket = _crewsSrvice.GetById(id);

            if (ticket == null) return NotFound();

            return Ok(ticket);
        }

        [HttpPost]
        public IActionResult Post([FromBody] CrewDto entity)
        {
            _crewsSrvice.Make(entity);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CrewDto entity)
        {
            _crewsSrvice.Update(entity);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _crewsSrvice.Delete(id);
            return Ok();
        }
    }
}
