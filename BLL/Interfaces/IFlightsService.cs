using AirportRESRfulApi.DAL.Models;
using AirportRESRfulApi.Shared.DTO;
using System;

namespace AirportRESRfulApi.BLL.Interfaces
{
    public interface IFlightsService : IService<Flight, FlightDto>
    {
        FlightDto GetByFlightNumberAndDate(string flightNumber, DateTime flightDate);

    }
}
