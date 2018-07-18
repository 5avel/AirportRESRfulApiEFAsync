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

namespace AirportRESRfulApi.BLL.Services
{
    public class TicketsService : Service<Ticket, TicketDto>, ITicketsService
    {
        private IValidator<TicketDto> _validator;
        public TicketsService(IUnitOfWork repository, IMapper mapper, IValidator<TicketDto> validator) : base(repository, mapper)
        {
            _validator = validator;
        }

        public TicketDto BuyById(int id)
        {
            var entity = _repository.Get(x => x.Id == id).SingleOrDefault();

            if (entity == null) return null;

            if (entity.IsSold == true) return null; // Уже был продан

            entity.IsSold = true;
            _repository.Update(entity);
            _unitOfWork.SaveChages();
            return _mapper.Map<Ticket, TicketDto>(entity);
        }

        public IEnumerable<TicketDto> GetNotSoldSByFlightIdAndDate(string flightNumber, DateTime flightDate)
        {
            Flight flight = _unitOfWork.Set<Flight>().Get().Where(x => x.FlightNumber == flightNumber & x.DepartureTime == flightDate).FirstOrDefault();

            if (flight == null) return null;

            var entity = _repository.Get().Where(t => t.FlightId == flight.Id);
            return _mapper.Map<IEnumerable<Ticket>, IEnumerable<TicketDto>>(entity);
        }

        public bool Make(IEnumerable<TicketDto> entitys)
        {
            _validator.Validate(entitys);
            var tickets = _mapper.Map<IEnumerable<TicketDto>, IEnumerable<Ticket>>(entitys);
            _repository.Create(tickets);
            _unitOfWork.SaveChages();
            return true; 
        }

        public TicketDto ReturnById(int id)
        {
            var entity = _repository.Get(x => x.Id == id).SingleOrDefault();

            if (entity == null) return null;

            if (entity.IsSold == false) return null;  // Уже был возвращен

            entity.IsSold = false;
            _repository.Update(entity);
            _unitOfWork.SaveChages();
            return _mapper.Map<Ticket, TicketDto>(entity);
        }
    }
}
