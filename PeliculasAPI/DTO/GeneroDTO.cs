using PeliculasAPI.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.DTO
{
    public class GeneroDTO
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
    }
}
