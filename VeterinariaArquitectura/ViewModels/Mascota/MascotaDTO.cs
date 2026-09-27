namespace VeterinariaArquitectura.ViewModels.Mascota
{
    public class MascotaDTO
    {
        public DateOnly FechaNacimiento { get; set; }
        public string Nombre { get; set; } = null!;
        public string NombrePropietario { get; set; } = null!;
        public string Raza { get; set; } = null!;
        public string Especie { get; set; } = null!;
        public string Color { get; set; } = null!;
        public decimal Peso { get; set; } 
        public string Sexo { get; set; } = null!;
    }
}
