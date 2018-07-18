using AirportRESRfulApi.BLL.Interfaces;
using AirportRESRfulApi.DAL.Interfaces;
using AirportRESRfulApi.DAL.Models;
using AirportRESRfulApi.Shared.DTO;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

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

        public virtual void Delete(int id)
        {
            _repository.Delete(id);
            _unitOfWork.SaveChages();
        }

        public virtual IEnumerable<TEntityDto> Get()
        {
            var entity = _repository.Get();
            return _mapper.Map<IEnumerable<TEntity>, IEnumerable<TEntityDto>>(entity);
        }

        public virtual TEntityDto GetById(int id)
        {
            var entity = _repository.Get(x => x.Id == id).SingleOrDefault();
            return _mapper.Map<TEntity, TEntityDto>(entity);
        }

        public virtual void Make(TEntityDto entity)
        {
            var makedEntity = _mapper.Map<TEntityDto, TEntity>(entity);
            _repository.Create(makedEntity);
            _unitOfWork.SaveChages();
        }

        public virtual void Update(TEntityDto entity)
        {
            var updatedEntity = _mapper.Map<TEntityDto, TEntity>(entity);
            _repository.Update(updatedEntity);
            _unitOfWork.SaveChages();


        }
    }
}
