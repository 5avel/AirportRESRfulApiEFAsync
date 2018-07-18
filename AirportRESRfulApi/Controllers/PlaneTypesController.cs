using AirportRESRfulApi.BLL.Interfaces;
using AirportRESRfulApi.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

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
        public IActionResult Get()
        {
            return Ok(_planeTypesService.Get());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var ticket = _planeTypesService.GetById(id);

            if (ticket == null) return NotFound();

            return Ok(ticket);
        }

        [HttpPost]
        public IActionResult Post([FromBody] PlaneTypeDto entity)
        {
            _planeTypesService.Make(entity);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PlaneTypeDto entity)
        {
            _planeTypesService.Update(entity);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _planeTypesService.Delete(id);
            return Ok();
        }
    }
}
