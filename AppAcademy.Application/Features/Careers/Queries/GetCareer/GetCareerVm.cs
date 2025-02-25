namespace AppAcademy.Application.Features.Careers.Queries.GetCareer
{
    public class GetCareerVm
    {
        public int CareerId { get; set; }
        public string Nombre { get; set; }
        public int DuracionSemestres { get; set; }
        public decimal CostoMensual { get; set; }
        public string Activa { get; set; }
    }
}
