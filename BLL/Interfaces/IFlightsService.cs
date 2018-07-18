using AirportRESRfulApi.DAL.Models;
using AirportRESRfulApi.Shared.DTO;
using System;
using System.Threading.Tasks;

namespace AirportRESRfulApi.BLL.Interfaces
{
    public interface IFlightsService : IService<Flight, FlightDto>
    {
        Task<FlightDto> GetByFlightNumberAndDate(string flightNumber, DateTime flightDate);
    }
}
