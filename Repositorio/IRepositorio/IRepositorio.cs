using System.Linq.Expressions;

namespace PersonaCompleta.Repositorio.IRepositorio
{
    public interface IRepositorio<T> where T:class
    {
        Task Crear(T entidad);
        //El signo de pregunta implica que si no se envia un filtro devuelve toda la lista, no seria obligatorio el filtro
        Task<List<T>> ObtenerTodos(Expression<Func<T,bool>>? filtro=null);


        Task<T> Obtener(Expression<Func<T, bool>>? filtro = null, bool track =true);


        Task Remove(T entidad);

        Task Grabar();
    }
}
