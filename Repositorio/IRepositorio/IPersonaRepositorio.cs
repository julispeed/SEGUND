using PersonaCompleta.Models;
namespace PersonaCompleta.Repositorio.IRepositorio
{
    public interface IPersonaRepositorio : IRepositorio<Persona>
    {
        Task<Persona> Actualizar(Persona perso);      

    }
}
