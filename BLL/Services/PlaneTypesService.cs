using AirportRESRfulApi.BLL.Interfaces;
using AirportRESRfulApi.DAL.Interfaces;
using AirportRESRfulApi.DAL.Models;
using AirportRESRfulApi.Shared.DTO;
using AutoMapper;
using FluentValidation;
using System.Threading.Tasks;

namespace AirportRESRfulApi.BLL.Services
{
    public class PlaneTypesService : Service<PlaneType, PlaneTypeDto>, IPlaneTypesService
    {
        private IValidator<PlaneTypeDto> _validator;
        public PlaneTypesService(IUnitOfWork repository, IMapper mapper, IValidator<PlaneTypeDto> validator) : base(repository, mapper)
        {
            _validator = validator;
        }

        public override async Task<PlaneTypeDto> AddAsync(PlaneTypeDto entity)
        {
            if (_validator.Validate(entity).IsValid)
                return await base.AddAsync(entity);
            return null;
        }

        public override async Task<PlaneTypeDto> UpdateAsync(PlaneTypeDto entity, int id)
        {
            if (_validator.Validate(entity).IsValid)
                return await base.UpdateAsync(entity, id);
            return null;
        }
    }
}
