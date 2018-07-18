using AirportRESRfulApi.BLL.Interfaces;
using AirportRESRfulApi.DAL.Interfaces;
using AirportRESRfulApi.DAL.Models;
using AirportRESRfulApi.Shared.DTO;
using AutoMapper;
using FluentValidation;
using System.Threading.Tasks;

namespace AirportRESRfulApi.BLL.Services
{
    public class PilotsService : Service<Pilot, PilotDto>, IPilotsService
    {
        private IValidator<PilotDto> _validator;
        public PilotsService(IUnitOfWork repository, IMapper mapper, IValidator<PilotDto> validator) : base(repository, mapper)
        {
            _validator = validator;
        }

        public override async Task<PilotDto> AddAsync(PilotDto entity)
        {
            if (_validator.Validate(entity).IsValid)
                return await base.AddAsync(entity);
            return null;
        }

        public override async Task<PilotDto> UpdateAsync(PilotDto entity, int id)
        {
            if (_validator.Validate(entity).IsValid)
                return await base.UpdateAsync(entity, id);
            return null;
        }
    }
}
