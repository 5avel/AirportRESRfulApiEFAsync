using AirportRESRfulApi.BLL.Interfaces;
using AirportRESRfulApi.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Get()
        {
            return Ok(await _crewsSrvice.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var entity = await _crewsSrvice.GetAsync(id);

            if (entity == null) return NotFound();

            return Ok(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CrewDto entity)
        {
            var result = await _crewsSrvice.AddAsync(entity);

            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CrewDto entity)
        {
            var result = await _crewsSrvice.UpdateAsync(entity, id);

            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async  Task<IActionResult> Delete([FromBody] CrewDto entity)
        {
            var result = await _crewsSrvice.DeleteAsync(entity);

            return Ok();
        }
    }
}
