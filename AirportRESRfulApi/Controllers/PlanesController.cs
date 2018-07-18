using AirportRESRfulApi.BLL.Interfaces;
using AirportRESRfulApi.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AirportRESRfulApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanesController : ControllerBase
    {
        private IPlanesService _planesSrvice;
        public PlanesController(IPlanesService planesSrvice)
        {
            _planesSrvice = planesSrvice;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _planesSrvice.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var entity = await _planesSrvice.GetAsync(id);

            if (entity == null) return NotFound();

            return Ok(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PlaneDto entity)
        {
            var result = await _planesSrvice.AddAsync(entity);

            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] PlaneDto entity)
        {
            var result = await _planesSrvice.UpdateAsync(entity, id);

            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromBody] PlaneDto entity)
        {
            var result = await _planesSrvice.DeleteAsync(entity);

            return Ok();
        }
    }
}
