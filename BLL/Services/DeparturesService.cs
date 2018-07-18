using AirportRESRfulApi.BLL.Interfaces;
using AirportRESRfulApi.DAL.Interfaces;
using AirportRESRfulApi.DAL.Models;
using AirportRESRfulApi.Shared.DTO;
using AutoMapper;
using FluentValidation;
using System.Threading.Tasks;

namespace AirportRESRfulApi.BLL.Services
{
    public class DeparturesService : Service<Departure, DepartureDto>, IDeparturesService
    {
        private IValidator<DepartureDto> _validator;
        public DeparturesService(IUnitOfWork repository, IMapper mapper, IValidator<DepartureDto> validator) : base(repository, mapper)
        {
            _validator = validator;
        }

        public override async Task<DepartureDto> AddAsync(DepartureDto entity)
        {
            if (_validator.Validate(entity).IsValid)
                return await base.AddAsync(entity);
            return null;
        }

        public override async Task<DepartureDto> UpdateAsync(DepartureDto entity, int id)
        {
            if (_validator.Validate(entity).IsValid)
                return await base.UpdateAsync(entity, id);
            return null;
        }
    }
}
