namespace PeliculasAPI
{
    public class ConexionBD
    {
        private string cadenaConexionSql;
        public string CadenaConexionSQL { get => cadenaConexionSql; }

        public  ConexionBD(string cadenaConexionSql)
        {
            this.cadenaConexionSql = cadenaConexionSql;
        }
    }
}
