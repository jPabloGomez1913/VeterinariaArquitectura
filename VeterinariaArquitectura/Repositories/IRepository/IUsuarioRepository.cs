using VeterinariaArquitectura.Models;

namespace VeterinariaArquitectura.Repositories.IRepository
{
    public interface IUsuarioRepository
    {
        public Task<bool> Create(Usuario usuario);
        public Task<bool> Update(Usuario usuario);
        public Task<bool> Delete(Usuario usuario);
        public Task<Usuario?> Get(int usuarioId);
        public Task<Usuario?> Existe(string correo, string clave);
    }
}
