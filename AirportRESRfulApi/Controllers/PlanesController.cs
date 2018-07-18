using AirportRESRfulApi.BLL.Interfaces;
using AirportRESRfulApi.Shared.DTO;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Get()
        {
            return Ok(_planesSrvice.Get());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var ticket = _planesSrvice.GetById(id);

            if (ticket == null) return NotFound();

            return Ok(_planesSrvice.GetById(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] PlaneDto entity)
        {
            _planesSrvice.Make(entity);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PlaneDto entity)
        {
            _planesSrvice.Update(entity);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _planesSrvice.Delete(id);
            return Ok();
        }
    }
}
