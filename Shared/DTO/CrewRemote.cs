using System;
using System.Collections.Generic;
using System.Text;

namespace AirportRESRfulApi.Shared.DTO
{
    public class CrewRemote: BaseDto
    {
        public List<PilotDto> pilot { get; set; }
        public List<StewardessDto> stewardess { get; set; }
    }
}
