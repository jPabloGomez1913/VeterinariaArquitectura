using System.ComponentModel.DataAnnotations;

namespace VeterinariaArquitectura.ViewModels.Usuario
{
    public class UsuarioEdicionVM
    {
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "El nombre completo es obligatorio.")]
        public string NombreCompleto { get; set; } = null!;

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "Ingrese un correo válido.")]
        public string Correo { get; set; } = null!;

        [Required(ErrorMessage = "El número de documento es obligatorio.")]
        public string NumeroDocumento { get; set; } = null!;

        [Required(ErrorMessage = "El tipo de documento es obligatorio.")]
        public string TipoDocumento { get; set; }

        [DataType(DataType.Password)]
        public string? Clave { get; set; }

        [DataType(DataType.Password)]
        [Compare(nameof(Clave), ErrorMessage = "Las contraseñas deben coincidir.")]
        public string? ConfirmarClave { get; set; }
    }
}
