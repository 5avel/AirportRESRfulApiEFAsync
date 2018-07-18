namespace AirportRESRfulApi.DAL.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class Plane : Entity
    {
        [Required, MaxLength(50)]
        public string Name { set; get; }
        [Required]
        public DateTime ReleaseDate { set; get; }


        public int DepartureId { get; set; }
        public Departure Departure { get; set; }


        public int PlaneTypeId { get; set; }
        public PlaneType PlaneType { set; get; }
    }
}
