using Dapper;
using PeliculasAPI.Entidades;
using System.Data.SqlClient;
using System.Data;

namespace PeliculasAPI.Servicios
{
    public class ServicioActor: IServicioActor
    {
        private string CadenaConexionSQL;

        //inyectamos por dependecia la conexion
        public ServicioActor(ConexionBD con)
        {
            CadenaConexionSQL = con.CadenaConexionSQL;
        }

        private SqlConnection conexion()
        {
            return new SqlConnection(CadenaConexionSQL);
        }

        public async Task deletActor(Actor a)
        {
            SqlConnection sqlConexion = conexion();

            try
            {
                sqlConexion.Open();
                var param = new DynamicParameters();
                param.Add("@Id", a.Id, DbType.Int32, ParameterDirection.Input);

                await sqlConexion.ExecuteScalarAsync("ActorBorrar", param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw new Exception("Se produjo un error al borrar un Actor");
            }
            finally
            {
                sqlConexion.Close();
                sqlConexion.Dispose();
            }
        }

        public async Task editActor(Actor a)
        {
            SqlConnection sqlConexion = conexion();
            try
            {
                //abrimos la conexion
                sqlConexion.Open();
                //creamos parametros
                var param = new DynamicParameters();
                param.Add("@Nombre", a.Nombre, DbType.String, ParameterDirection.Input, 150);
                param.Add("@FechaNacimiento", a.FechaNacimiento, DbType.DateTime, ParameterDirection.Input);
                param.Add("@Foto", a.Foto, DbType.String, ParameterDirection.Input);
                param.Add("@Id", a.Id, DbType.Int32, ParameterDirection.Input);

                //como no devuelve valores ejecutamos
                await sqlConexion.ExecuteScalarAsync("ActorModificar", param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                //log.LogError("Error: " + ex.ToString());
                throw new Exception("Se produjo un error al modificar Actor ");
            }
            finally
            {
                sqlConexion.Close();
                sqlConexion.Dispose();
            }
        }

        public async Task<IEnumerable<Actor>> getAllActores()
        {
            SqlConnection sqlConexion = conexion();
            List<Actor> actores = new List<Actor>();
            try
            {
                //abrimos la conexion
                sqlConexion.Open();
                //devuelve una lista de empleados
                var r = await sqlConexion.QueryAsync<Actor>("ActorObtener", commandType: CommandType.StoredProcedure);
                actores = r.ToList();
            }
            catch (Exception ex)
            {
                //log.LogError("Error: " + ex.ToString());
                throw new Exception("Se produjo un error al obtener los Actores ");
            }
            finally
            {
                sqlConexion.Close();
                sqlConexion.Dispose();
            }

            return actores;
        }

        public async Task<Actor> getByIdActor(int id)
        {
            SqlConnection sqlConexion = conexion();
            Actor a = null;
            try
            {
                //abrimos la conexion
                sqlConexion.Open();
                //creamos parametros
                var param = new DynamicParameters();
                param.Add("@Id", id, DbType.Int32, ParameterDirection.Input);

                //aqui devuelve un empleado si lo encuentra
                a = await sqlConexion.QueryFirstOrDefaultAsync<Actor>("ActorObtener", param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                //log.LogError("Error: " + ex.ToString());
                throw new Exception("Se produjo un error al obtener actor ");
            }
            finally
            {
                sqlConexion.Close();
                sqlConexion.Dispose();
            }

            return a;
        }

        public async Task newActor(Actor a)
        {
            SqlConnection sqlConexion = conexion();
            try
            {
                //abrimos la conexion
                sqlConexion.Open();
                //creamos parametros
                var param = new DynamicParameters();
                param.Add("@Nombre", a.Nombre, DbType.String, ParameterDirection.Input, 150);
                param.Add("@FechaNacimiento", a.FechaNacimiento, DbType.DateTime, ParameterDirection.Input);
                param.Add("@Foto", a.Foto, DbType.String, ParameterDirection.Input);

                //como no devuelve valores ejecutamos
                await sqlConexion.ExecuteScalarAsync("ActorAlta", param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                //log.LogError("Error: " + ex.ToString());
                throw new Exception("Se produjo un error al dar de alta Actor ");
            }
            finally
            {
                sqlConexion.Close();
                sqlConexion.Dispose();
            }
        }
    }
}
