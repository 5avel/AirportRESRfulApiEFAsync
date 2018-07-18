using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AirportRESRfulApi.BLL.Interfaces
{
    public interface ICrewsRemoteService
    {
        Task LoadCrews();
    }
}
