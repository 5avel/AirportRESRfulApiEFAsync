using AirportRESRfulApi.BLL.Interfaces;
using AirportRESRfulApi.DAL.Interfaces;
using AirportRESRfulApi.DAL.Models;
using AirportRESRfulApi.Shared.DTO;
using AutoMapper;
using FluentValidation;

namespace AirportRESRfulApi.BLL.Services
{
    public class CrewsService : Service<Crew, CrewDto>, ICrewsService
    {
        private IValidator<CrewDto> _validator;
        public CrewsService(IUnitOfWork repository, IMapper mapper, IValidator<CrewDto> validator) : base(repository, mapper)
        {

            _validator = validator;
        }

        public override void Make(CrewDto entity)
        {
            if (_validator.Validate(entity).IsValid)
                base.Make(entity);
        }

        public override void Update(CrewDto entity)
        {
            if (_validator.Validate(entity).IsValid)
                base.Update(entity);
        }
    }
}
