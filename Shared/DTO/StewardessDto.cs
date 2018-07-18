namespace AirportRESRfulApi.Shared.DTO
{
    using System;
    public class StewardessDto : BaseDto
    {
        public int CrewId { set; get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
    }
}