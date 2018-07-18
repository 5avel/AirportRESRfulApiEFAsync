using AirportRESRfulApi.DAL.Models;
using System;
using System.Threading.Tasks;

namespace AirportRESRfulApi.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> Set<TEntity>() where TEntity : Entity;

        //int SaveChages();

        Task<int> SaveChangesAsync();
    }
}
