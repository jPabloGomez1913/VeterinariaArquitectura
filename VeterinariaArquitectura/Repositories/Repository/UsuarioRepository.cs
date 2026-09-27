using Microsoft.EntityFrameworkCore;
using VeterinariaArquitectura.Data;
using VeterinariaArquitectura.Models;
using VeterinariaArquitectura.Repositories.IRepository;

namespace VeterinariaArquitectura.Repositories.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            return await Guardar();
        }

        public async Task<bool> Delete(Usuario usuario)
        {
            _context.Remove(usuario);
            return await Guardar();

        }

        public async Task<Usuario?> Existe(string correo, string clave)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(x => x.Clave == clave && x.Correo == correo);
        }

        public async Task<Usuario?> Get(int usuarioId)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(x => x.UsuarioId == usuarioId);
        }

        public async Task<IReadOnlyList<Usuario>> GetAll()
        {
            return await _context.Usuarios.AsNoTracking()
                .OrderBy(x => x.NombreCompleto)
                .ToListAsync();
        }

        public async Task<bool> Update(Usuario usuario)
        {
            _context.Update(usuario);
            return await Guardar();
        }
        private async Task<bool> Guardar()
        {
            return await _context.SaveChangesAsync() >= 1;
        }
    }
}
