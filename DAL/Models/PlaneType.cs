namespace AirportRESRfulApi.DAL.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class PlaneType : Entity
    {
        [Required, MaxLength(50)]
        public string Model { set; get; }
        [Required]
        public int Seats { set; get; }
        [Required]
        public int Capacity { set; get; }
        [Required]
        public int Range { set; get; }
        [Required]
        public Int64 ServiceLifeInTicks { set; get; }
        [NotMapped]
        public TimeSpan ServiceLife
        {
            get { return TimeSpan.FromTicks(ServiceLifeInTicks); }
            set { ServiceLifeInTicks = value.Ticks; }
        }
    }
}