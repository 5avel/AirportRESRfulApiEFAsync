using AirportRESRfulApi.BLL.Interfaces;
using AirportRESRfulApi.DAL.Interfaces;
using AirportRESRfulApi.DAL.Models;
using AirportRESRfulApi.Shared.DTO;
using AutoMapper;
using FluentValidation;
using System;
using System.Linq;

namespace AirportRESRfulApi.BLL.Services
{
    public class FlightsService : Service<Flight, FlightDto>, IFlightsService
    {
        private IValidator<FlightDto> _validator;

        public FlightsService(IUnitOfWork repository, IMapper mapper, IValidator<FlightDto> validator) : base(repository, mapper)
        {
            _validator = validator;
        }

        public override void Make(FlightDto entity)
        {
            if (_validator.Validate(entity).IsValid)
                base.Make(entity);
        }

        public override void Update(FlightDto entity)
        {
            if(_validator.Validate(entity).IsValid)
                base.Update(entity);
        }

        public FlightDto GetByFlightNumberAndDate(string flightNumber, DateTime flightDate)
        {
            var result = _repository.Get(x => x.FlightNumber == flightNumber & x.DepartureTime == flightDate).SingleOrDefault();
            return _mapper.Map<Flight, FlightDto>(result);
        }
    }
}
