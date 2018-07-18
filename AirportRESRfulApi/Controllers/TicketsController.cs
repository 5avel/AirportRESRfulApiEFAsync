using AirportRESRfulApi.BLL.Interfaces;
using AirportRESRfulApi.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirportRESRfulApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private ITicketsService _ticketSrvice;
        public TicketsController(ITicketsService ticketSrvice)
        {
            _ticketSrvice = ticketSrvice;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _ticketSrvice.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var entity = await _ticketSrvice.GetAsync(id);

            if (entity == null) return NotFound();

            return Ok(entity);
        }

        // GET http://localhost:5000/api/Tickets/QW11/2018-07-13T08:22:56.6404304+03:00
        [HttpGet("{flightId}/{flightDate}")]
        public async Task<IActionResult> Get(string flightId, DateTime flightDate)
        {
            if (String.IsNullOrWhiteSpace(flightId)) return NotFound("flightDate Is Null Or WhiteSpace!");
            if (flightDate == null) return NotFound("flightDate is null!");

            var tickets = await _ticketSrvice.GetNotSoldSByFlightIdAndDateAsync(flightId, flightDate);

            if (tickets == null || tickets.Count() == 0) return NotFound();

            return Ok(tickets);
        }

        // GET http://localhost:5000/api/Tickets/Bay/2
        [HttpGet("Bay/{id}")]
        public async Task<IActionResult> BayById(int id)
        {
            var result = await _ticketSrvice.BuyByIdAsync(id);

            if (result == null) return NotFound();

            return Ok(result);
        }

        //GET http://localhost:5000/api/Tickets/Return/2
        [HttpGet("Return/{id}")]
        public async Task<IActionResult> ReturnById(int id)
        {
            var result = await _ticketSrvice.ReturnByIdAsync(id);

            if (result == null) return NotFound();

            return Ok(result);
        }

    }
}
