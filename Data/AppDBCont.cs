using Microsoft.EntityFrameworkCore;
using PersonaCompleta.Controllers;
using PersonaCompleta.Models;
namespace PersonaCompleta.Data
{
    public class AppDBCont :DbContext
    {

        public AppDBCont(DbContextOptions <AppDBCont> options) : base(options) 
        {
        }
        public DbSet<Persona> Documento { get; set; }
       
        public DbSet<Secundario> Secundario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Persona>().HasData
                (
                    new Persona()
                    {
                        id = 1,
                        nombre = "Julián",
                        segundonombre = "Martin",
                        apellido = "Acuña",
                        sexo = 'M',
                        edad = 20,
                        documento = 44120148,
                        nacionalidad="ARG",
                        imagen="",
                        fecha=DateTime.Now,
                        LastUpdated=DateTime.Now
    }   
                );
        }

    }
}
