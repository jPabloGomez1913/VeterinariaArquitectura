using Microsoft.EntityFrameworkCore;
using VeterinariaArquitectura.Data;
using VeterinariaArquitectura.Models;
using VeterinariaArquitectura.Repositories.IRepository;
using VeterinariaArquitectura.ViewModels.Mascota;

namespace VeterinariaArquitectura.Repositories.Repository
{
    public class MascotaRepository : IMascotaRepository
    {
        private readonly ApplicationDbContext _context;

        public MascotaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(Mascota mascota)
        {
            await _context.AddAsync(mascota);
            
        }

        public void Delete(Mascota mascota)
        {
             _context.Remove(mascota);

        }

        public async Task<Mascota?> Get(int mascotaId)
        {

            return await _context.Mascota.FirstOrDefaultAsync(x => x.MascotaId == mascotaId);
        }

        public async Task<ICollection<Mascota?>> GetAll()
        {
            return await _context.Mascota.ToListAsync();
        }

        public void  Update(Mascota mascota)
        {
            _context.Update(mascota);
           
        }
        public async Task<bool> Guardar()
        {
            return await _context.SaveChangesAsync() >= 1;
        }

        public async Task<EditarMascotaDTO?> GetActualizar(int mascotaId)
        {
            var mascota = await _context.Mascota.Where(wh => wh.MascotaId == mascotaId)
                .Select(s => new EditarMascotaDTO
                {
                    MascotaId = s.MascotaId,
                    Nombre = s.Nombre,
                    NombrePropietario = s.NombrePropietario,
                    NumeroPropietario = s.NumeroPropietario,
                    CorreoPropietario = s.CorreoPropietario,
                    FechaNacimiento = s.FechaNacimiento
                }).FirstOrDefaultAsync();
            return mascota;
        }
    }
}
