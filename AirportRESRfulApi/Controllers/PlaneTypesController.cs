using AirportRESRfulApi.BLL.Interfaces;
using AirportRESRfulApi.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace AirportRESRfulApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaneTypesController : ControllerBase
    {
        private IPlaneTypesService _planeTypesService;
        public PlaneTypesController(IPlaneTypesService planeTypesService)
        {
            _planeTypesService = planeTypesService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _planeTypesService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var entity = await _planeTypesService.GetAsync(id);

            if (entity == null) return NotFound();

            return Ok(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PlaneTypeDto entity)
        {
            var result = await _planeTypesService.AddAsync(entity);

            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] PlaneTypeDto entity)
        {
            var result = await _planeTypesService.UpdateAsync(entity, id);

            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromBody] PlaneTypeDto entity)
        {
            var result = await _planeTypesService.DeleteAsync(entity);

            return Ok();
        }
    }
}
