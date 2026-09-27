using VeterinariaArquitectura.Common;
using VeterinariaArquitectura.Models;
using VeterinariaArquitectura.ViewModels.Mascota;

namespace VeterinariaArquitectura.Services.IService
{
    public interface IMascotaService
    {
        Task<Response<Mascota>> Crear(MascotaDTO mascotaDTO);
        Task<Response<Mascota>> Editar(EditarMascotaDTO editarMascotaDTO);
        Task<Response<ICollection<Mascota>>> Listar();
        Task<Response<Mascota?>> Get(int mascotaId);
        Task<Response> Eliminar(int mascotaId);
        Task<Response<EditarMascotaDTO>> GetEditar(int mascotaId);

    }
}
