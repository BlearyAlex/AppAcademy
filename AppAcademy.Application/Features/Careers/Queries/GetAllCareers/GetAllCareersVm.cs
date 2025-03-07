namespace AppAcademy.Application.Features.Careers.Queries.GetAllCareers
{
    public class GetAllCareersVm
    {
        public int CareerId { get; set; }
        public string Nombre { get; set; }
        public int DuracionMeses { get; set; }
        public decimal CostoMensual { get; set; }
        public string Color { get; set; }
        public bool Activa { get; set; }
    }
}
