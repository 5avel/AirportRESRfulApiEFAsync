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
    public class StewardessesController : ControllerBase
    {
        private IStewardessesService _stewardessesSrvice;
        public StewardessesController(IStewardessesService stewardessesSrvice)
        {
            _stewardessesSrvice = stewardessesSrvice;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _stewardessesSrvice.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var entity = await _stewardessesSrvice.GetAsync(id);

            if (entity == null) return NotFound();

            return Ok(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] StewardessDto entity)
        {
            var result = await _stewardessesSrvice.AddAsync(entity);

            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] StewardessDto entity)
        {
            var result = await _stewardessesSrvice.UpdateAsync(entity, id);

            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] StewardessDto entity)
        {
            var result = await _stewardessesSrvice.DeleteAsync(entity);

            return Ok();
        }
    }
}
