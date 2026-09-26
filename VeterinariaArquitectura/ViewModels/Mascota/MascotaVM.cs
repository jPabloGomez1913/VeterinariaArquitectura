namespace VeterinariaArquitectura.ViewModels.Mascota
{
    public class MascotaVM
    {
        public DateOnly FechaNacimiento { get; set; }
        public string Nombre { get; set; } = null!;
        public string NombrePropietario { get; set; } = null!;
        public string CorreoPropietario { get; set; } = null!;
        public string NumeroPropietario { get; set; } = null!;
    }
}
