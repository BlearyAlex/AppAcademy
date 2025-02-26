using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Permissions.Commands.CreatePermission
{
    public class CreatePermissionCommand : IRequest<int>
    {
        public int PermissionId { get; set; }
        public string Nombre { get; set; }  // Ejemplo: "Beca", "Descuento", "Permiso de ausencia"
        public string Descripcion { get; set; }
        public bool Activo { get; set; }  // TRUE si el permiso sigue vigente
    }
}
