using Microsoft.EntityFrameworkCore;
using RegistrarCedula.Shared;

namespace RegistrarCedula.Data
{
    public class RegistrarCedulaContext : DbContext
    {
        public RegistrarCedulaContext(DbContextOptions options) : base(options) { }

        public DbSet<Persona> Persona { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite();
        }
    }
}
