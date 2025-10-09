using Microsoft.AspNetCore.Antiforgery;
using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.Entidades
{
    public class Actor
    {
        //En lugar de ser pudiera poner init, el cual solo iniciara la primera
        //vez el id autonumerico y no se podra cambiar
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string? Foto { get; set; }
    }
}
