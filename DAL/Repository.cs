using AirportRESRfulApi.DAL.Interfaces;
using AirportRESRfulApi.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AirportRESRfulApi.DAL
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        private readonly AirportContext context;

        public Repository(AirportContext context)
        {
            this.context = context;
        }

        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = context.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.ToList();
        }

        public virtual TEntity GetById(int id)
        {
            IQueryable<TEntity> query = context.Set<TEntity>();

            var entity = query.SingleOrDefault(x => x.Id == id);

            return entity;
        }

        public virtual void Create(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
        }

        public virtual void Create(IEnumerable<TEntity> entitys)
        {
            context.Set<TEntity>().AddRange(entitys);
        }

        public virtual void Update(TEntity entity)
        {
            context.Set<TEntity>().Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(object id)
        {
            TEntity entity = context.Set<TEntity>().Find(id);
            if(entity != null)
                Delete(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            var dbSet = context.Set<TEntity>();
            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }

        #region Async
        public virtual async Task<ICollection<TEntity>> GetAllAsync()
        {
            return await context.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<TEntity> GetAsync(int id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            await context.Set<TEntity>().AddAsync(entity);
            return entity;
        }
        

        public virtual async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await context.Set<TEntity>().SingleOrDefaultAsync(filter);
        }

        public virtual async Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await context.Set<TEntity>().Where(filter).ToListAsync();
        }

        public virtual async Task<int> DeleteAsync(TEntity entity)
        {
            if (entity == null) return 0;

            TEntity exist = await context.Set<TEntity>().FindAsync(entity.Id);

            context.Set<TEntity>().Remove(exist);
            return entity.Id;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity, object key)
        {
            if (entity == null) return null;

            TEntity exist = await context.Set<TEntity>().FindAsync(key);
            if (exist != null)
            {
                context.Entry(exist).CurrentValues.SetValues(entity);
                await context.SaveChangesAsync();
            }
            return exist;
        }

        #endregion Async
    }
}
