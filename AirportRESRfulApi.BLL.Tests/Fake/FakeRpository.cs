using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AirportRESRfulApi.DAL.Interfaces;
using AirportRESRfulApi.DAL.Models;

namespace AirportRESRfulApi.BLL.Tests.Fake
{
    class FakeRpository<T> : IRepository<T> where T : Entity
    {
        public T createdItem;
        public IEnumerable<T> createdItems;
        public T updatedItem;

        public void Create(T entity)
        {
            createdItem = entity;
        }

        public void Create(IEnumerable<T> entitys)
        {
            createdItems = entitys;
        }

        public void Delete(object id)
        {
            
        }

        public void Delete(T entity)
        {
            
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            updatedItem = entity;
        }
    }
}
