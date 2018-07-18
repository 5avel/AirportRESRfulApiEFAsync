using AirportRESRfulApi.DAL.Interfaces;
using AirportRESRfulApi.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AirportRESRfulApi.BLL.Tests.Fake
{
    class FakeUnitOfWork<T> : IUnitOfWork where T : Entity
    {
        private IRepository<T> repository;
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public int SaveChages()
        {
            return 0;
        }

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public IRepository<TEntity> Set<TEntity>() where TEntity : Entity
        {
            if (repository == null) repository = new FakeRpository<T>();
            return (IRepository<TEntity>)repository;
        }

        public void ClearData()
        {
            var rep = repository as FakeRpository<T>;
            rep.createdItem = null;
            rep.createdItems = null;
            rep.updatedItem = null;
        }
    }
}
