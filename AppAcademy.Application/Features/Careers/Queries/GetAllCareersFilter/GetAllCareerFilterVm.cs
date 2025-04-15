namespace AppAcademy.Application.Features.Careers.Queries.GetAllCareersFilter
{
    public class GetAllCareerFilterVm
    {
        public int CareerId { get; set; }
        public string Nombre { get; set; }
        public int DuracionMeses { get; set; }
        public decimal CostoMensual { get; set; }
        public string Color { get; set; }
        public bool Activa { get; set; }
    }
}
