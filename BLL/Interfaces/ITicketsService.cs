using AirportRESRfulApi.DAL.Models;
using AirportRESRfulApi.Shared.DTO;
using System;
using System.Collections.Generic;

namespace AirportRESRfulApi.BLL.Interfaces
{
    public interface ITicketsService : IService<Ticket, TicketDto>
    {
        IEnumerable<TicketDto> GetNotSoldSByFlightIdAndDate(string flightNumber, DateTime flightDate);
        TicketDto BuyById(int id);
        TicketDto ReturnById(int id);
        bool Make(IEnumerable<TicketDto> entitys);
       
    }
}
