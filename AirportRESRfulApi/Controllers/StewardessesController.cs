using AirportRESRfulApi.BLL.Interfaces;
using AirportRESRfulApi.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AirportRESRfulApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StewardessesController : ControllerBase
    {
        private IStewardessesService _stewardessesSrvice;
        public StewardessesController(IStewardessesService stewardessesSrvice)
        {
            _stewardessesSrvice = stewardessesSrvice;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_stewardessesSrvice.Get());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var entity = _stewardessesSrvice.GetById(id);

            if (entity == null) return NotFound();

            return Ok(entity);
        }

        [HttpPost]
        public IActionResult Post([FromBody] StewardessDto entity)
        {
            _stewardessesSrvice.Make(entity);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] StewardessDto entity)
        {
            _stewardessesSrvice.Update(entity);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _stewardessesSrvice.Delete(id);
            return Ok();
        }
    }
}
