using System;
using System.Reflection;
using System.Threading.Tasks;
using AirportRESRfulApi.DAL.Interfaces;
using AirportRESRfulApi.DAL.Models;

namespace AirportRESRfulApi.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly AirportContext context;
        

        public UnitOfWork(AirportContext context)
        {
            this.context = context;
        }
        

        //public int SaveChages()
        //{
        //    return context.SaveChanges();
        //}

        public Task<int> SaveChangesAsync()
        {
            return context.SaveChangesAsync();
        }

        public IRepository<TEntity> Set<TEntity>() where TEntity : Entity
        {
             return new Repository<TEntity>(context);
        }



        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
                this.disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
