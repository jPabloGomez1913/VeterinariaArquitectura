using VeterinariaArquitectura.Models;

namespace VeterinariaArquitectura.Repositories.IRepository
{
    public interface IMascotaRepository
    {
        Task<bool> Create(Mascota mascota);
        Task<bool> Update(Mascota mascota);
        Task<bool> Delete(Mascota mascota);
        Task<Mascota?> Get(int mascotaId);
        Task<ICollection<Mascota?>> GetAll();

    }
}
