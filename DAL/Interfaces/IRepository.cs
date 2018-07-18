using AirportRESRfulApi.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AirportRESRfulApi.DAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : Entity
    {

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
