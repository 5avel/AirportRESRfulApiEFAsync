using AirportRESRfulApi.BLL.Interfaces;
using AirportRESRfulApi.BLL.Validations;
using AirportRESRfulApi.DAL.Interfaces;
using AirportRESRfulApi.DAL.Models;
using AirportRESRfulApi.Shared.DTO;
using AutoMapper;
using FluentValidation;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirportRESRfulApi.BLL.Services
{
    public class TicketsService : Service<Ticket, TicketDto>, ITicketsService
    {
        private IValidator<TicketDto> _validator;
        public TicketsService(IUnitOfWork repository, IMapper mapper, IValidator<TicketDto> validator) : base(repository, mapper)
        {
            _validator = validator;
        }

        public async Task<IEnumerable<TicketDto>> GetNotSoldSByFlightIdAndDateAsync(string flightNumber, DateTime flightDate)
        {
            Flight flight = await _unitOfWork.Set<Flight>().FindAsync(x => x.FlightNumber == flightNumber & x.DepartureTime == flightDate);

            if (flight == null) return null;

            var entitys = await _repository.FindAllAsync(t => t.FlightId == flight.Id);
            return _mapper.Map<IEnumerable<Ticket>, IEnumerable<TicketDto>>(entitys);
        }

        public async Task<TicketDto> ReturnByIdAsync(int id)
        {
            var entity = await _repository.FindAsync(x => x.Id == id);

            if (entity == null) return null;

            if (entity.IsSold == false) return null;  // Уже был возвращен

            entity.IsSold = false;
            
            return await base.UpdateAsync(_mapper.Map<Ticket, TicketDto>(entity), id);
        }

        public async Task<TicketDto> BuyByIdAsync(int id)
        {
            var entity = await _repository.FindAsync(x => x.Id == id);

            if (entity == null) return null;

            if (entity.IsSold == true) return null; // Уже был продан

            entity.IsSold = true;

            return await base.UpdateAsync(_mapper.Map<Ticket, TicketDto>(entity), id);
        }
    }
}
