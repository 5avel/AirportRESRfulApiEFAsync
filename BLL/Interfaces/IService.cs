using AirportRESRfulApi.DAL.Models;
using AirportRESRfulApi.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirportRESRfulApi.BLL.Interfaces
{
    public interface IService<TEntity, TEntityDto> where TEntity : Entity where TEntityDto : BaseDto
    {
        void Delete(int id);
        IEnumerable<TEntityDto> Get();
        TEntityDto GetById(int id);
        void Make(TEntityDto entity);
        void Update(TEntityDto entity);
    }
}
