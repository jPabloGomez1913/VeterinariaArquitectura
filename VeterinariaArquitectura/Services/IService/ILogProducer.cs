using VeterinariaArquitectura.Models;

namespace VeterinariaArquitectura.Services.IService
{
    public interface ILogProducer
    {
        void Publicar(LogEvento evento);
    }
}
