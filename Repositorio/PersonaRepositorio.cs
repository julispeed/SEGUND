using PersonaCompleta.Repositorio.IRepositorio;
using PersonaCompleta.Models;
using PersonaCompleta.Data;

namespace PersonaCompleta.Repositorio
{
    public class PersonaRepositorio : Repositorio<Persona>, IPersonaRepositorio
    {
        private readonly AppDBCont _db;

        public PersonaRepositorio (AppDBCont db) : base(db)
        {
            _db = db;
            
        }
        public async Task<Persona> Actualizar(Persona perso)
        {
            perso.LastUpdated = DateTime.Now;
            _db.Update(perso);
            await _db.SaveChangesAsync();
            return perso;
        }
    }
}
