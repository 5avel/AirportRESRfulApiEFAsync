using AirportRESRfulApi.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AirportRESRfulApi.DAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null);
        void Create(TEntity entity);
        void Create(IEnumerable<TEntity> entitys);
        void Update(TEntity entity);
        void Delete(object id);
        void Delete(TEntity entity);

     #region Async
        Task<ICollection<TEntity>> GetAllAsync();
        Task<TEntity> GetAsync(int id);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> filter);
        Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match);
        Task<int> DeleteAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity, object key);
     #endregion Async
    }
}
