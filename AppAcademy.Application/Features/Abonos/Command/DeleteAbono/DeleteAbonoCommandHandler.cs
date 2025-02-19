using AppAcademy.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Abonos.Command.DeleteAbono
{
    public class DeleteAbonoCommandHandler : IRequestHandler<DeleteAbonoCommand, string>
    {
        private readonly IAbonoRepository _abonoRepository;

        public DeleteAbonoCommandHandler(IAbonoRepository abonoRepository)
        {
            _abonoRepository = abonoRepository;
        }

        public async Task<string> Handle(DeleteAbonoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Llamar al repositorio para eliminar el abono
                bool eliminado = await _abonoRepository.DeleteAbono(request.AbonoId);

                if (eliminado)
                {
                    return $"Abono con ID {request.AbonoId} eliminado exitosamente.";
                }
                else
                {
                    return $"No se pudo eliminar el abono con ID {request.AbonoId}.";
                }
            }
            catch (Exception ex)
            {
                // Manejar excepciones y retornar mensaje adecuado
                return $"Error al eliminar el abono: {ex.Message}";
            }
        }
    }
}
