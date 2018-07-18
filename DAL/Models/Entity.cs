namespace AirportRESRfulApi.DAL.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    public class Entity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}
