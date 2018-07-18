namespace AirportRESRfulApi.DAL.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class Stewardess : Entity
    {

        public int CrewId { set; get; }
        public Crew Crew { get; set; }

        [Required, MaxLength(100), MinLength(3)]
        public string FirstName { get; set; }
        [Required, MaxLength(100), MinLength(3)]
        public string LastName { get; set; }
        [Required]
        public DateTime Birthday { get; set; }

        
    }
}