using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirportRESRfulApi.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AirportRESRfulApi.Controllers
{
    [Route("api/[controller]")]
    public class CrewsRemoteController : Controller
    {
        ICrewsRemoteService _service;
        public CrewsRemoteController(ICrewsRemoteService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            await _service.LoadCrews();
            return Ok();
        }
    }
}