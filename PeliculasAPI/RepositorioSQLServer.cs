using PeliculasAPI.Entidades;

namespace PeliculasAPI
{
    public class RepositorioSQLServer : IRepositorio
    {
        public void Crear(Genero genero)
        {
            throw new NotImplementedException();
        }

        public bool Existe(string nombre)
        {
            throw new NotImplementedException();
        }

        public List<Genero> ObtenerAllGeneros()
        {
            throw new NotImplementedException();
        }

        public Task<Genero?> ObtnerById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
