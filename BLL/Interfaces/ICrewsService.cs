using AirportRESRfulApi.DAL.Models;
using AirportRESRfulApi.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AirportRESRfulApi.BLL.Interfaces
{
    public interface ICrewsService : IService<Crew, CrewDto>
    {

        Task<CrewDto> FindeAsync(Expression<Func<Crew, bool>> filter);

    }
}
