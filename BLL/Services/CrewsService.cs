using AirportRESRfulApi.BLL.Interfaces;
using AirportRESRfulApi.DAL.Interfaces;
using AirportRESRfulApi.DAL.Models;
using AirportRESRfulApi.Shared.DTO;
using AutoMapper;
using FluentValidation;
using System.Threading.Tasks;

namespace AirportRESRfulApi.BLL.Services
{
    public class CrewsService : Service<Crew, CrewDto>, ICrewsService
    {
        private IValidator<CrewDto> _validator;
        public CrewsService(IUnitOfWork repository, IMapper mapper, IValidator<CrewDto> validator) : base(repository, mapper)
        {
            _validator = validator;
        }

        public override async Task<CrewDto> AddAsync(CrewDto entity)
        {
            if (_validator.Validate(entity).IsValid)
                return await base.AddAsync(entity);
            return null;
        }

        public override async Task<CrewDto> UpdateAsync(CrewDto entity, int id)
        {
            if (_validator.Validate(entity).IsValid)
                return await base.UpdateAsync(entity, id);
            return null;
        }
    }
}
