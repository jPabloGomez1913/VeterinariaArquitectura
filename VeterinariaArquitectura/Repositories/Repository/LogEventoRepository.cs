using VeterinariaArquitectura.Data;
using VeterinariaArquitectura.Models;
using VeterinariaArquitectura.Repositories.IRepository;

namespace VeterinariaArquitectura.Repositories.Repository
{
    public class LogEventoRepository : ILogEventoRepository
    {
        private readonly ApplicationDbContext _context;

        public LogEventoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(LogEvento logEvento)
        {
            await _context.AddAsync(logEvento);
        }

        public async Task<bool> Guardar()
        {
            return await _context.SaveChangesAsync() >= 1;
        }
    }
}
