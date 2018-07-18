using AirportRESRfulApi.BLL.Interfaces;
using AirportRESRfulApi.DAL.Interfaces;
using AirportRESRfulApi.DAL.Models;
using AirportRESRfulApi.Shared.DTO;
using AutoMapper;
using FluentValidation;
using System.Threading.Tasks;

namespace AirportRESRfulApi.BLL.Services
{
    public class StewardessesService : Service<Stewardess, StewardessDto>, IStewardessesService
    {
        private IValidator<StewardessDto> _validator;
        public StewardessesService(IUnitOfWork repository, IMapper mapper, IValidator<StewardessDto> validator) : base(repository, mapper)
        {
            _validator = validator;
        }

        public override async Task<StewardessDto> AddAsync(StewardessDto entity)
        {
            if (_validator.Validate(entity).IsValid)
                return await base.AddAsync(entity);
            return null;
        }

        public override async Task<StewardessDto> UpdateAsync(StewardessDto entity, int id)
        {
            if (_validator.Validate(entity).IsValid)
                return await base.UpdateAsync(entity, id);
            return null;
        }
    }
}
