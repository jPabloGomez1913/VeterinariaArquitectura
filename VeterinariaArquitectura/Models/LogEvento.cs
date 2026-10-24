namespace VeterinariaArquitectura.Models
{
    public class LogEvento
    {
        public int LogEventoId { get; set; }
        public string Proceso { get; set; } = null!;
        public string Accion { get; set; } = null!;
        public string Nivel { get; set; } = "Information";
        public string Mensaje { get; set; } = null!;
        public DateTime Fecha { get; set; }
        public string? Usuario { get; set; }
    }
}
