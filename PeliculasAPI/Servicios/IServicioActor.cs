using PeliculasAPI.Entidades;

namespace PeliculasAPI.Servicios
{
    public interface IServicioActor
    {
        public Task<IEnumerable<Actor>> getAllActores();
        public Task<Actor> getByIdActor(int id);
        public Task newActor(Actor a);
        public Task editActor(Actor a);
        public Task deletActor(Actor a);
    }
}
