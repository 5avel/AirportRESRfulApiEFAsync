using AirportRESRfulApi.BLL.Interfaces;
using AirportRESRfulApi.DAL.Interfaces;
using AirportRESRfulApi.DAL.Models;
using AirportRESRfulApi.Shared.DTO;
using AutoMapper;
using FluentValidation;

namespace AirportRESRfulApi.BLL.Services
{
    public class DeparturesService : Service<Departure, DepartureDto>, IDeparturesService
    {
        private IValidator<DepartureDto> _validator;
        public DeparturesService(IUnitOfWork repository, IMapper mapper, IValidator<DepartureDto> validator) : base(repository, mapper)
        {
            _validator = validator;
        }

        public override void Make(DepartureDto entity)
        {
            if (_validator.Validate(entity).IsValid)
                base.Make(entity);
        }

        public override void Update(DepartureDto entity)
        {
            if (_validator.Validate(entity).IsValid)
                base.Update(entity);
        }
    }
}
