using AirportRESRfulApi.BLL.Interfaces;
using AirportRESRfulApi.DAL.Interfaces;
using AirportRESRfulApi.DAL.Models;
using AirportRESRfulApi.Shared.DTO;
using AutoMapper;
using FluentValidation;
using System.Threading.Tasks;

namespace AirportRESRfulApi.BLL.Services
{
    public class PlanesService : Service<Plane, PlaneDto>, IPlanesService
    {
        private IValidator<PlaneDto> _validator;
        public PlanesService(IUnitOfWork repository, IMapper mapper, IValidator<PlaneDto> validator) : base(repository, mapper)
        {
            _validator = validator;
        }

        public override async Task<PlaneDto> AddAsync(PlaneDto entity)
        {
            if (_validator.Validate(entity).IsValid)
                return await base.AddAsync(entity);
            return null;
        }

        public override async Task<PlaneDto> UpdateAsync(PlaneDto entity, int id)
        {
            if (_validator.Validate(entity).IsValid)
                return await base.UpdateAsync(entity, id);
            return null;
        }
    }
}
