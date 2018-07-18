namespace AirportRESRfulApi.DAL.Models
{

    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Ticket: Entity
    {
        [ForeignKey("FlightId")]
        public int FlightId { set; get; }

        [Required, MaxLength(8)]
        public string FlightNumber { set; get; }
        [Required]
        public decimal Price { set; get; }
        [Required]
        public int PlaseNumber { set; get; }
        [Required]
        public bool IsSold { set; get; }
    }
}