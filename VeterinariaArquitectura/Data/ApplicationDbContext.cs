using Microsoft.EntityFrameworkCore;
using VeterinariaArquitectura.Models;

namespace VeterinariaArquitectura.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
       : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Mascota> Mascota { get; set; }
    }
}
