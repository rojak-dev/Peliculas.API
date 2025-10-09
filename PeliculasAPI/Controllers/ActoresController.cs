using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PeliculasAPI.DTO;
using PeliculasAPI.Entidades;
using PeliculasAPI.Servicios;

namespace PeliculasAPI.Controllers
{
    [Route("api/actores")]
    [ApiController]
    public class ActoresController: ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IServicioActor _servicioActor;
        private readonly IAlmacenadorArchivos almacenadorArchivo;
        private readonly string contenedor = "actores";
        public ActoresController(IMapper mapper, IServicioActor servicioActor, IAlmacenadorArchivos almacenadorArchivos)
        {
            this.mapper = mapper;
            this._servicioActor = servicioActor;
            this.almacenadorArchivo = almacenadorArchivos;
        }

        [HttpGet("ObtenerTodos")]
        public async Task<IEnumerable<ActorDTO>> GetActores()
        {
            var listaActores = await _servicioActor.getAllActores();
            var actoresDTO = mapper.Map<List<ActorDTO>>(listaActores);
            return actoresDTO;
        }

        [HttpGet("{id:int}", Name = "ObtenerActorPorId")]
        public async Task<ActionResult<ActorDTO>> Get(int id)
        {
            var actor = await _servicioActor.getByIdActor(id);
            var actoroDTO = mapper.Map<ActorDTO>(actor);

            if (actor is null)
            {
                return NotFound();
            }

            return actoroDTO;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] ActorCreacionDTO actorCreacionDTO) //recibira el archivo desde el actorcreacionDTO
        {
            var actor = mapper.Map<Actor>(actorCreacionDTO);

            if (actorCreacionDTO.Foto is not null)
            {
                var url = await almacenadorArchivo.Almacenar(contenedor, actorCreacionDTO.Foto);
                actor.Foto = url;
            }

            await _servicioActor.newActor(actor);
            return CreatedAtRoute("ObtenerActorPorId", new { id = actor.Id}, actor);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromForm] ActorCreacionDTO actorCreacionDTO)
        {
            var actor = await _servicioActor.getByIdActor(id);

            if (actor is null)
            {
                return NotFound();
            }

            //actualizar con la misma instancia del objeto
            actor = mapper.Map(actorCreacionDTO, actor);

            if (actorCreacionDTO.Foto is not null)
            {
                actor.Foto = await almacenadorArchivo.Editar(actor.Foto, contenedor, actorCreacionDTO.Foto);
            }

            await _servicioActor.editActor(actor);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var actor = await _servicioActor.getByIdActor(id);

            if (actor is null)
            {
                return NotFound();
            }

            await _servicioActor.deletActor(actor);

            return NoContent();
        }
    }
}
