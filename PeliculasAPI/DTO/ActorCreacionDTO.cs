using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.DTO
{
    public class ActorCreacionDTO
    {
        [Required]
        [StringLength(150)]
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public IFormFile? Foto { get; set; }
    }
}
