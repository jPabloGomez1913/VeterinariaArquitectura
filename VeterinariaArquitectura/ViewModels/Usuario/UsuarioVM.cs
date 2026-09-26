namespace VeterinariaArquitectura.ViewModels.Usuario
{
    public class UsuarioVM
    {
        public string NombreCompleto { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string NumeroDocumento { get; set; } = null!;
        public int TipoDocumento { get; set; }
        public string Clave { get; set; } = null!;
        public string ConfirmarClave { get; set; } = null!;
    }
}
