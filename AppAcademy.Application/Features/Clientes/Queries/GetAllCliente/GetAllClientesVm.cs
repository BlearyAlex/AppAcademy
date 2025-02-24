namespace AppAcademy.Application.Features.Clientes.Queries.GetAllCliente
{
    public class GetAllClientesVm
    {
        public string ClienteId { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Email { get; set; }
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
