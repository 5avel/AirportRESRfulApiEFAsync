using AirportRESRfulApi.BLL.Interfaces;
using AirportRESRfulApi.DAL.Interfaces;
using AirportRESRfulApi.DAL.Models;
using AirportRESRfulApi.Shared.DTO;
using AutoMapper;
using FluentValidation;

namespace AirportRESRfulApi.BLL.Services
{
    public class StewardessesService : Service<Stewardess, StewardessDto>, IStewardessesService
    {
        private IValidator<StewardessDto> _validator;
        public StewardessesService(IUnitOfWork repository, IMapper mapper, IValidator<StewardessDto> validator) : base(repository, mapper)
        {
            _validator = validator;
        }

        public override void Make(StewardessDto entity)
        {
            if (_validator.Validate(entity).IsValid)
                base.Make(entity);
        }

        public override void Update(StewardessDto entity)
        {
            if (_validator.Validate(entity).IsValid)
                base.Update(entity);
        }
    }
}
