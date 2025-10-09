namespace PeliculasAPI.Servicios
{
    public interface IAlmacenadorArchivos
    {
        Task<string> Almacenar(string contenedor, IFormFile archivo);
        Task borrar(string? ruta, string contenedor);
        async Task<string> Editar(string? ruta, string contenedor, IFormFile archivo)
        {
            await borrar(ruta, contenedor);

            return await Almacenar(contenedor, archivo);
        }
    }
}
