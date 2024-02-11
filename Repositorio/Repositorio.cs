using PersonaCompleta.Controllers;
using PersonaCompleta.Data;
using Microsoft.EntityFrameworkCore;
using PersonaCompleta.Repositorio.IRepositorio;
using System.Linq.Expressions;

namespace PersonaCompleta.Repositorio
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {

        private readonly AppDBCont _db;
        internal DbSet<T> DbSet;
        public Repositorio(AppDBCont db)
        {
            _db = db;
            this.DbSet = _db.Set<T>();
        }

        public async Task Crear(T entidad)
        {
            await DbSet.AddAsync(entidad);
            await Grabar();
        }

        public async Task Grabar()
        {
            await _db.SaveChangesAsync();
        }

        public async Task<T> Obtener(Expression<Func<T, bool>>? filtro = null, bool track = true)
        {

            IQueryable<T> query = DbSet;
            if (!track)
            {
                query = query.AsNoTracking();
            }
            if (filtro != null)
            {
                query = query.Where(filtro);
            }
            return await query.FirstOrDefaultAsync();

        }

        public async Task<List<T>> ObtenerTodos(Expression<Func<T, bool>>? filtro = null)
        {
            IQueryable<T> query = DbSet;
            if (filtro != null)
            {
                query = query.Where(filtro);
            }
            return await query.ToListAsync();
        }
    
        public async Task Remove(T entidad)
        {
            DbSet.Remove(entidad);
            await Grabar();
        }
    }
}
