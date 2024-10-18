using MediatR;

namespace AppAcademy.Application.Features.Entradas.Commands.DeleteEntrada
{
    public class DeleteEntradaCommand : IRequest
    {
        public string EntradaId { get; set; }

        public DeleteEntradaCommand(string entradaId)
        {
            EntradaId = entradaId;
        }
    }
}
