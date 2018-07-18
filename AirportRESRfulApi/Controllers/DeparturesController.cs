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
    public class DeparturesController : ControllerBase
    {
        private IDeparturesService _departuresSrvice;
        public DeparturesController(IDeparturesService departuresSrvice)
        {
            _departuresSrvice = departuresSrvice;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _departuresSrvice.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var entity = await _departuresSrvice.GetAsync(id);

            if (entity == null) return NotFound();

            return Ok(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DepartureDto entity)
        {
            var result = await _departuresSrvice.AddAsync(entity);

            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] DepartureDto entity)
        {
            var result = await _departuresSrvice.UpdateAsync(entity, id);

            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromBody] DepartureDto entity)
        {
            var result = await _departuresSrvice.DeleteAsync(entity);

            return Ok();
        }
    }
}
