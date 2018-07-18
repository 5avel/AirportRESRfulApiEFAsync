using AirportRESRfulApi.DAL.Models;
using AirportRESRfulApi.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirportRESRfulApi.BLL.Interfaces
{
    public interface ITicketsService : IService<Ticket, TicketDto>
    {
        Task<IEnumerable<TicketDto>> GetNotSoldSByFlightIdAndDateAsync(string flightNumber, DateTime flightDate);
        Task<TicketDto> BuyByIdAsync(int id);
        Task<TicketDto> ReturnByIdAsync(int id);
       
    }
}
