using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PeliculasAPI.DTO;
using PeliculasAPI.Entidades;
using PeliculasAPI.Servicios;
using PeliculasAPI.Utilidades;
using System.Threading.Tasks;

namespace PeliculasAPI.Controllers
{
    [Route("api/generos")] //se recomienda dejar la ruta a tu gusto por su en dado caso se requiere cambiar en el futuro
    [ApiController] //verificador de reglas de validacion
    public class GenerosController: ControllerBase
    {
        

        //provedoor de configuracion inyectado por dependencia
        private readonly IConfiguration configuration;

        private readonly IServicioGenero _servicioGenero;
        private readonly IMapper mapper;

        //inyeccin de dependencias en el constructur (recordar asignar el servicio en el program)
        public GenerosController(IMapper mapper, IConfiguration configuration, IServicioGenero servicioGenero)
        {
            
            this.configuration = configuration;
            this._servicioGenero = servicioGenero;
            this.mapper = mapper;
        }

        [HttpGet("ObtenerTodos")]
        public async Task<ActionResult<IEnumerable<GeneroDTO>>> GetGeneros([FromQuery] PaginacionDTO paginacion)
        {
            //los metadatos (totalRegistros, paginaActual, totalPaginas) se eliminan del body y los pasamos al header, para devolver una lista GenerosDTO


            var resultado = await _servicioGenero.ObtenerGenerosPaginados(paginacion);

            //agregar el total de registros en los Headers
            Response.Headers.Append("cantidad-total-registros", resultado.TotalRegistros.ToString());

            if (resultado.Registros.Count == 0)
                return NotFound("No hay registros disponibles");

            
            return resultado.Registros;
        }

        /*[HttpGet("ObtenerTodos")]
        public async Task<IEnumerable<GeneroDTO>> GetGeneros()
        {
            var listaGeneros = await _servicioGenero.getAllGeneros();
            var generosDTO = mapper.Map<List<GeneroDTO>>(listaGeneros);
            return generosDTO;
        }*/

        [HttpGet("{id:int}", Name = "ObtenerGeneroPorId")]//se define una variable de ruta
        public async Task<ActionResult<GeneroDTO>> GetGenero(int id) //para obtener un tipo action result en caso que no se encuentre el objeto con programacion asyncrona
        {

            var genero = await _servicioGenero.getByIdGenero(id);
            var generoDTO = mapper.Map<GeneroDTO>(genero);

            if (genero is null)
            {
                return NotFound();
            }

            return generoDTO;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]GeneroCreacionDTO generoDTO) //indicamos que queremos la data desde el cuerpo de la peticion y no por la URL
        {
            //usando AutoMapper
            var gen = mapper.Map<Genero>(generoDTO);

            await _servicioGenero.newGenero(gen);
            return CreatedAtRoute("ObtenerGeneroPorId", new { id = gen.Id }, gen);
            //return CreatedAtAction(nameof(GetGenero), new { id= gen.Id}, gen);

            /*validacion en el controlador
            var yaexisiteUngeneroConDichoNombre = repositorio.Existe(genero.Nombre);

            if (yaexisiteUngeneroConDichoNombre)
            {
                return BadRequest($"Ya existe un genero con el nombre: {genero.Nombre}");
            }

            repositorio.Crear(genero);

            return Ok();*/

        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] GeneroCreacionDTO generoCreacionDTO)
        {
            var genero = await _servicioGenero.getByIdGenero(id);

            if (genero is null)
            {
                return NotFound();
            }

            genero = mapper.Map<Genero>(generoCreacionDTO);
            genero.Id = id;

            await _servicioGenero.editGenero(genero);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var genero = await _servicioGenero.getByIdGenero(id);

            if (genero is null)
            {
                return NotFound();
            }

            await _servicioGenero.deletGenero(genero);

            return NoContent();
        }
    }
}
