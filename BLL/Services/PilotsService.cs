using AirportRESRfulApi.BLL.Interfaces;
using AirportRESRfulApi.DAL.Interfaces;
using AirportRESRfulApi.DAL.Models;
using AirportRESRfulApi.Shared.DTO;
using AutoMapper;
using FluentValidation;

namespace AirportRESRfulApi.BLL.Services
{
    public class PilotsService : Service<Pilot, PilotDto>, IPilotsService
    {
        private IValidator<PilotDto> _validator;
        public PilotsService(IUnitOfWork repository, IMapper mapper, IValidator<PilotDto> validator) : base(repository, mapper)
        {
            _validator = validator;
        }

        public override void Make(PilotDto entity)
        {
            if (_validator.Validate(entity).IsValid)
                base.Make(entity);
        }

        public override void Update(PilotDto entity)
        {
            if (_validator.Validate(entity).IsValid)
                base.Update(entity);
        }
    }
}
