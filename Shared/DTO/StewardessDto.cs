namespace AirportRESRfulApi.Shared.DTO
{
    using Newtonsoft.Json;
    using System;
    public class StewardessDto : BaseDto
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int CrewId { set; get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}