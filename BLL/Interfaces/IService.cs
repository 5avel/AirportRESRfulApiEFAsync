using AirportRESRfulApi.DAL.Models;
using AirportRESRfulApi.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AirportRESRfulApi.BLL.Interfaces
{
    public interface IService<TEntity, TEntityDto> where TEntity : Entity where TEntityDto : BaseDto
    {
        Task<int> DeleteAsync(TEntityDto entity);
        Task<ICollection<TEntityDto>> GetAllAsync();
        Task<TEntityDto> GetAsync(int id);
        Task<TEntityDto> AddAsync(TEntityDto entity);
        Task<TEntityDto> UpdateAsync(TEntityDto entity, int key);
    }
}
