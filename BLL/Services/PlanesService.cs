using AirportRESRfulApi.BLL.Interfaces;
using AirportRESRfulApi.DAL.Interfaces;
using AirportRESRfulApi.DAL.Models;
using AirportRESRfulApi.Shared.DTO;
using AutoMapper;
using FluentValidation;

namespace AirportRESRfulApi.BLL.Services
{
    public class PlanesService : Service<Plane, PlaneDto>, IPlanesService
    {
        private IValidator<PlaneDto> _validator;
        public PlanesService(IUnitOfWork repository, IMapper mapper, IValidator<PlaneDto> validator) : base(repository, mapper)
        {
            _validator = validator;
        }

        public override void Make(PlaneDto entity)
        {
            if (_validator.Validate(entity).IsValid)
                base.Make(entity);
        }

        public override void Update(PlaneDto entity)
        {
            if (_validator.Validate(entity).IsValid)
                base.Update(entity);
        }
    }
}
