using AirportRESRfulApi.BLL.Interfaces;
using AirportRESRfulApi.DAL.Interfaces;
using AirportRESRfulApi.DAL.Models;
using AirportRESRfulApi.Shared.DTO;
using AutoMapper;
using FluentValidation;

namespace AirportRESRfulApi.BLL.Services
{
    public class PlaneTypesService : Service<PlaneType, PlaneTypeDto>, IPlaneTypesService
    {
        private IValidator<PlaneTypeDto> _validator;
        public PlaneTypesService(IUnitOfWork repository, IMapper mapper, IValidator<PlaneTypeDto> validator) : base(repository, mapper)
        {
            _validator = validator;
        }

        public override void Make(PlaneTypeDto entity)
        {
            if (_validator.Validate(entity).IsValid)
                base.Make(entity);
        }

        public override void Update(PlaneTypeDto entity)
        {
            if (_validator.Validate(entity).IsValid)
                base.Update(entity);
        }
    }
}
