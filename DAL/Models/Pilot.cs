namespace AirportRESRfulApi.DAL.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class Pilot : Entity
    {
        [Required, MaxLength(100)]
        public string FirstName { get; set; }
        [Required, MaxLength(100)]
        public string LastName { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        [Required]
        public int Experience { set; get; }

        public int CrewId { set; get; }
        public Crew Crew { get; set; }
    }
}