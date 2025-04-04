namespace AppAcademy.Domain.Logs
{
    public class Bitacora
    {
        public string BitacoraId { get; set; } = Guid.NewGuid().ToString();
        public DateTime Fecha { get; set; }
        public string UsuarioId { get; set; }
        public string Descripcion { get; set; }
        public string? ReferenciaId { get; set; }
        public string TipoReferencia { get; set; } // "Student", "Payment", "AbonoAcademy"
    }
}
