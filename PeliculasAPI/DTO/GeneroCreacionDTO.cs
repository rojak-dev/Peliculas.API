using PeliculasAPI.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.DTO
{
    public class GeneroCreacionDTO
    {
        //validaciones
        [Required(ErrorMessage = "El campo {0} es requerido")] //validacion con ASP.NET.CORE
        [StringLength(50, ErrorMessage = "El campo {0} debe tener {1} caracteres o menos")] //validacion de maximo de caracteres
        [PrimeraLetraMayuscula] //validacion por atributo personalizada creada en la carpeta validaciones
        public required string Nombre { get; set; }
    }
}
