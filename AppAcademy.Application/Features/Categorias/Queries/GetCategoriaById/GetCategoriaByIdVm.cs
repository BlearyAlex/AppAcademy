namespace AppAcademy.Application.Features.Categorias.Queries.GetCategoriaById
{
    public class GetCategoriaByIdVm
    {
        public string CategoriaId { get; set; }
        public string? Nombre { get; set; }
        public string? Color { get; set; }
        public string? Description { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
