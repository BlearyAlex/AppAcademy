namespace AppAcademy.Domain.ControlAcademia
{
    public class Career
    {
        public int CareerId { get; set; }
        public string Nombre { get; set; }
        public int DuracionMeses { get; set; }
        public decimal CostoMensual { get; set; }
        public bool Activa { get; set; }

        public virtual List<Student> Estudiantes { get; set; }
    }
}
