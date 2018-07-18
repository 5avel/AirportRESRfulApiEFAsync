using AirportRESRfulApi.BLL.Interfaces;
using AirportRESRfulApi.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public IActionResult Get()
        {
            return Ok(_ticketSrvice.Get());
        }

        // GET api/Tickets/2
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var ticket = _ticketSrvice.GetById(id);

            if (ticket == null) return NotFound();

            return Ok(ticket);
        }

        // GET http://localhost:5000/api/Tickets/QW11/2018-07-13T08:22:56.6404304+03:00
        [HttpGet("{flightId}/{flightDate}")]
        public IActionResult Get(string flightId, DateTime flightDate)
        {
            if (String.IsNullOrWhiteSpace(flightId)) return NotFound("flightDate Is Null Or WhiteSpace!");
            if (flightDate == null) return NotFound("flightDate is null!");

            var tickets = _ticketSrvice.GetNotSoldSByFlightIdAndDate(flightId, flightDate);

            if (tickets == null || tickets.Count() == 0) return NotFound();

            return Ok(tickets);
        }

        // GET http://localhost:5000/api/Tickets/Bay/2
        [HttpGet("Bay/{id}")]
        public IActionResult BayById(int id)
        {
            var result = _ticketSrvice.BuyById(id);

            if (result == null) return NotFound();

            return Ok(result);
        }

        //GET http://localhost:5000/api/Tickets/Return/2
        [HttpGet("Return/{id}")]
        public IActionResult ReturnById(int id)
        {
            var result = _ticketSrvice.ReturnById(id);

            if (result == null) return NotFound();

            return Ok(result);
        }

    }
}
