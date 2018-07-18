using AirportRESRfulApi.BLL.Interfaces;
using AirportRESRfulApi.DAL.Interfaces;
using AirportRESRfulApi.DAL.Models;
using AirportRESRfulApi.Shared.DTO;
using AutoMapper;
using FluentValidation;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AirportRESRfulApi.BLL.Services
{
    public class FlightsService : Service<Flight, FlightDto>, IFlightsService
    {
        private IValidator<FlightDto> _validator;

        public FlightsService(IUnitOfWork repository, IMapper mapper, IValidator<FlightDto> validator) : base(repository, mapper)
        {
            _validator = validator;
        }

        public override async Task<FlightDto> AddAsync(FlightDto entity)
        {
            if (_validator.Validate(entity).IsValid)
                return await base.AddAsync(entity);
            return null;
        }

        public override async Task<FlightDto> UpdateAsync(FlightDto entity, int id)
        {
            if (_validator.Validate(entity).IsValid)
                return await base.UpdateAsync(entity, id);
            return null;
        }

        public async Task<FlightDto> GetByFlightNumberAndDate(string flightNumber, DateTime flightDate)
        {
            var result = await _repository.FindAsync(x => x.FlightNumber == flightNumber & x.DepartureTime == flightDate);
            return _mapper.Map<Flight, FlightDto>(result);
        }
    }
}
