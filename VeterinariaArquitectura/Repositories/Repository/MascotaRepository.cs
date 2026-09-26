using Microsoft.EntityFrameworkCore;
using VeterinariaArquitectura.Data;
using VeterinariaArquitectura.Models;
using VeterinariaArquitectura.Repositories.IRepository;

namespace VeterinariaArquitectura.Repositories.Repository
{
    public class MascotaRepository : IMascotaRepository
    {
        private readonly ApplicationDbContext _context;

        public MascotaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(Mascota mascota)
        {
            await _context.AddAsync(mascota);
            return await Guardar();
        }

        public async Task<bool> Delete(Mascota mascota)
        {
             _context.Remove(mascota);
            return await Guardar();

        }

        public async Task<Mascota?> Get(int mascotaId)
        {

            return await _context.Mascota.FirstOrDefaultAsync(x => x.MascotaId == mascotaId);
        }

        public async Task<ICollection<Mascota?>> GetAll()
        {
            return await _context.Mascota.ToListAsync();
        }

        public async Task<bool> Update(Mascota mascota)
        {
            _context.Update(mascota);
            return await Guardar();
        }
        private async Task<bool> Guardar()
        {
            return await _context.SaveChangesAsync() >= 1;
        }
    }
}
