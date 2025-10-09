using PeliculasAPI.Entidades;

namespace PeliculasAPI
{
    public interface IRepositorio
    {
        List<Genero> ObtenerAllGeneros();

        Task<Genero?> ObtnerById(int id);

        public bool Existe(string nombre);

        public void Crear(Genero genero);
    }
}
