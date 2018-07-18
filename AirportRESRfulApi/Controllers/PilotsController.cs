using AirportRESRfulApi.BLL.Interfaces;
using AirportRESRfulApi.Shared.DTO;
using Microsoft.AspNetCore.Mvc;

namespace AirportRESRfulApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PilotsController : ControllerBase
    {
        private IPilotsService _pilotsSrvice;
        public PilotsController(IPilotsService pilotsSrvice)
        {
            _pilotsSrvice = pilotsSrvice;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_pilotsSrvice.Get());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var ticket = _pilotsSrvice.GetById(id);

            if (ticket == null) return NotFound();

            return Ok(_pilotsSrvice.GetById(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] PilotDto entity)
        {
            _pilotsSrvice.Make(entity);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PilotDto entity)
        {
            _pilotsSrvice.Update(entity);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _pilotsSrvice.Delete(id);
            return Ok();
        }
    }
}
