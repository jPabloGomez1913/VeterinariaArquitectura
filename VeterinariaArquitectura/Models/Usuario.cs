namespace VeterinariaArquitectura.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string NombreCompleto { get; set; } = null!;
        public string Correo { get; set; }= null!;
        public string NumeroDocumento { get; set; } = null!;
        public int TipoDocumento { get; set; }
        public string Clave { get; set; } = null!;
    }
}
