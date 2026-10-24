namespace VeterinariaArquitectura.Services.IService
{
    public interface IUsuarioActual
    {
        // Correo (o nombre) del usuario autenticado; null si no hay sesión.
        string? Identificador { get; }
    }
}
