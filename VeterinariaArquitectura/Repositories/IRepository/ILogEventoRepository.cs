using VeterinariaArquitectura.Models;

namespace VeterinariaArquitectura.Repositories.IRepository
{
    public interface ILogEventoRepository
    {
        Task Create(LogEvento logEvento);
        Task<bool> Guardar();
    }
}
