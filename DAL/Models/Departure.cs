namespace AirportRESRfulApi.DAL.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Departure : Entity
    {
        [Required, MaxLength(8)]
        public string FlightNumber { set; get; }
        [Required]
        public DateTime DepartureTime { set; get; }
        [ForeignKey("FlightId")]
        public int FlightId { set; get; }

        public Plane Plane { set; get; }

        public Crew Crew { set; get; }
    }
}
