using Microsoft.EntityFrameworkCore;

namespace TopSecretNicaAPICore.Models
{
    public class ApplicationDbContext : DbContext
    {
        // Constructor que recibe las opciones de configuración de la base de datos
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        // DbSet para la tabla 'Marcas' en la base de datos
        public DbSet<Marcas> Marcas { get; set; }
    }
}
