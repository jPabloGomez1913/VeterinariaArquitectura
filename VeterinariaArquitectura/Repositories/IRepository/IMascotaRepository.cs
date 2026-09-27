using VeterinariaArquitectura.Models;
using VeterinariaArquitectura.ViewModels.Mascota;

namespace VeterinariaArquitectura.Repositories.IRepository
{
    public interface IMascotaRepository
    {
        Task Create(Mascota mascota);
        void Update(Mascota mascota);
        void Delete(Mascota mascota);
        Task<Mascota?> Get(int mascotaId);
        Task<EditarMascotaDTO?> GetActualizar(int mascotaId);
        Task<ICollection<Mascota?>> GetAll();
        Task<bool> Guardar();

    }
}
