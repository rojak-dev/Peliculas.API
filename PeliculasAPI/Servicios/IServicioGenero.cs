using PeliculasAPI.DTO;
using PeliculasAPI.Entidades;
using PeliculasAPI.Utilidades;

namespace PeliculasAPI.Servicios
{
    public interface IServicioGenero
    {
        public Task<IEnumerable<Genero>> getAllGeneros();
        public Task<Genero> getByIdGenero(int id);
        public Task newGenero(Genero g);
        public Task editGenero(Genero g);
        public Task deletGenero(Genero g);
        public Task<PaginacionResultado<GeneroDTO>> ObtenerGenerosPaginados(PaginacionDTO paginacion);
    }
}
