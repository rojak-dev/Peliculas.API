using Dapper;
using PeliculasAPI.DTO;
using PeliculasAPI.Entidades;
using PeliculasAPI.Utilidades;
using System.Data;
using System.Data.SqlClient;

namespace PeliculasAPI.Servicios
{
    public class ServicioGenero : IServicioGenero
    {
        //realizamos la conexion a la base de datos
        private string CadenaConexionSQL;

        //inyectamos por dependecia la conexion
        public ServicioGenero(ConexionBD con)
        {
            CadenaConexionSQL = con.CadenaConexionSQL;
        }

        private SqlConnection conexion()
        {
            return new SqlConnection(CadenaConexionSQL);
        }

        public async Task deletGenero(Genero g)
        {
            SqlConnection sqlConexion = conexion();

            try
            {
                sqlConexion.Open();
                var param = new DynamicParameters();
                param.Add("@Id", g.Id, DbType.Int32, ParameterDirection.Input);

                await sqlConexion.ExecuteScalarAsync("GeneroBorrar", param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw new Exception("Se produjo un error al borrar un genero");
            }
            finally
            {
                sqlConexion.Close();
                sqlConexion.Dispose();
            }
        }

        public async Task editGenero(Genero g)
        {
            SqlConnection sqlConexion = conexion();
            try
            {
                //abrimos la conexion
                sqlConexion.Open();
                //creamos parametros
                var param = new DynamicParameters();
                param.Add("@Nombre", g.Nombre, DbType.String, ParameterDirection.Input, 50);
                param.Add("@Id", g.Id, DbType.Int32, ParameterDirection.Input);

                //como no devuelve valores ejecutamos
                await sqlConexion.ExecuteScalarAsync("GeneroModificar", param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                //log.LogError("Error: " + ex.ToString());
                throw new Exception("Se produjo un error al modificar empleado ");
            }
            finally
            {
                sqlConexion.Close();
                sqlConexion.Dispose();
            }
        }

        public async Task<IEnumerable<Genero>> getAllGeneros()
        {
            SqlConnection sqlConexion = conexion();
            List<Genero> empleados = new List<Genero>();
            try
            {
                //abrimos la conexion
                sqlConexion.Open();
                //devuelve una lista de empleados
                var r = await sqlConexion.QueryAsync<Genero>("GenerObtener", commandType: CommandType.StoredProcedure);
                empleados = r.ToList();
            }
            catch (Exception ex)
            {
                //log.LogError("Error: " + ex.ToString());
                throw new Exception("Se produjo un error al obtener los Generos ");
            }
            finally
            {
                sqlConexion.Close();
                sqlConexion.Dispose();
            }

            return empleados;
        }

        public async Task<PaginacionResultado<GeneroDTO>> ObtenerGenerosPaginados(PaginacionDTO paginacion)
        {
           using var sqlConexion = conexion();

            string queryCount = "SELECT COUNT(*) FROM Generos";
            int totalRegistros = await sqlConexion.ExecuteScalarAsync<int>(queryCount);

            string query = @"
                SELECT * FROM Generos 
                ORDER BY Id 
                OFFSET @Offset ROWS FETCH NEXT @RecordsPorPagina ROWS ONLY";

            var generos = await sqlConexion.QueryAsync<GeneroDTO>(
                query,
                new { Offset = (paginacion.Pagina - 1) * paginacion.RecordsPorPagina, RecordsPorPagina = paginacion.RecordsPorPagina }
            );

            return new PaginacionResultado<GeneroDTO>(generos.ToList(), totalRegistros, paginacion.Pagina, paginacion.RecordsPorPagina);
        }

        public async Task<Genero> getByIdGenero(int id)
        {
            SqlConnection sqlConexion = conexion();
            Genero e = null;
            try
            {
                //abrimos la conexion
                sqlConexion.Open();
                //creamos parametros
                var param = new DynamicParameters();
                param.Add("@IdGenero", id, DbType.Int32, ParameterDirection.Input, 1);

                //aqui devuelve un empleado si lo encuentra
                e = await sqlConexion.QueryFirstOrDefaultAsync<Genero>("GenerObtener", param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                //log.LogError("Error: " + ex.ToString());
                throw new Exception("Se produjo un error al obtener empleado ");
            }
            finally
            {
                sqlConexion.Close();
                sqlConexion.Dispose();
            }

            return e;
        }

        public async Task newGenero(Genero g)
        {
            SqlConnection sqlConexion = conexion();
            try
            {
                //abrimos la conexion
                sqlConexion.Open();
                //creamos parametros
                var param = new DynamicParameters();
                param.Add("@Nombre", g.Nombre, DbType.String, ParameterDirection.Input, 50);

                //como no devuelve valores ejecutamos
                await sqlConexion.ExecuteScalarAsync("GeneroAlta", param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                //log.LogError("Error: " + ex.ToString());
                throw new Exception("Se produjo un error al dar de alta ");
            }
            finally
            {
                sqlConexion.Close();
                sqlConexion.Dispose();
            }
        }
    }
}
