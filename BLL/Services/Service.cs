using AirportRESRfulApi.BLL.Interfaces;
using AirportRESRfulApi.DAL.Interfaces;
using AirportRESRfulApi.DAL.Models;
using AirportRESRfulApi.Shared.DTO;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirportRESRfulApi.BLL.Services
{
    public abstract class Service<TEntity, TEntityDto> : IService<TEntity, TEntityDto> where TEntity : Entity where TEntityDto : BaseDto
    {
        protected IRepository<TEntity> _repository;
        protected IUnitOfWork _unitOfWork;

        protected IMapper _mapper;

        public Service(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.Set<TEntity>();
            _mapper = mapper;
        }

        public virtual async Task<TEntityDto> AddAsync(TEntityDto entity)
        {
            TEntity makingEntity = _mapper.Map<TEntityDto, TEntity>(entity);
            TEntity makedEntity = await _repository.AddAsync(makingEntity);
            entity.Id = 0;
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<TEntity, TEntityDto>(makedEntity);
        }

        public virtual async Task<int> DeleteAsync(TEntityDto entity)
        {
            TEntity deletingEntity = _mapper.Map<TEntityDto, TEntity>(entity);

            var result = await _repository.DeleteAsync(deletingEntity);
            var saveResult = await _unitOfWork.SaveChangesAsync();

            return result;
        }

        public virtual async Task<ICollection<TEntityDto>> GetAllAsync()
        {
            var allEntity =  await _repository.GetAllAsync();
            var result = _mapper.Map<ICollection<TEntity>, ICollection<TEntityDto>>(allEntity);
            return result;
        }

        public virtual async Task<TEntityDto> GetAsync(int id)
        {
            var entity = await _repository.GetAsync(id);
            var result = _mapper.Map<TEntity, TEntityDto>(entity);
            return result;
        }

        public virtual async Task<TEntityDto> UpdateAsync(TEntityDto entity, int key)
        {
            TEntity updatingEntity = _mapper.Map<TEntityDto, TEntity>(entity);
            TEntity udatedEntity = await _repository.UpdateAsync(updatingEntity, key);

            var saveResult = await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<TEntity, TEntityDto>(udatedEntity);
        }
    }
}
