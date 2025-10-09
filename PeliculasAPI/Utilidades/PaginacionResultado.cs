namespace PeliculasAPI.Utilidades
{
    public class PaginacionResultado<T>
    {
        /*Clase para el resultado de la paginacion*/


        public List<T> Registros { get; set; }
        public int TotalRegistros { get; set; }
        public int PaginaActual { get; set; }
        public int TotalPaginas { get; set; }

        public PaginacionResultado(List<T> registros, int totalRegistros, int paginaActual, int recordsPorPagina)
        {
            Registros = registros;
            TotalRegistros = totalRegistros;
            PaginaActual = paginaActual;
            TotalPaginas = (int)Math.Ceiling((double)totalRegistros / recordsPorPagina);
        }
    }
}
