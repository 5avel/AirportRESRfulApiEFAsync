namespace AirportRESRfulApi.Shared.DTO
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    public class CrewDto : BaseDto
    {
        public int? DepartureId { get; set; }

        [JsonProperty(PropertyName = "id")]
        public int RemoteId { set; get; }

        [JsonProperty(PropertyName = "pilot")]
        public List<PilotDto> Pilots { get; set; }
        [JsonProperty(PropertyName = "stewardess")]
        public List<StewardessDto> Stewardesses { get; set; }
    }
}