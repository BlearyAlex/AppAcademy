using AppAcademy.Domain.ControlAcademia;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.StudentPermissions.Commands.UpdateStudentPermission
{
    public class UpdateStudentPermissionCommand : IRequest
    {
        public int StudentPermissionId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        public int? StudentId { get; set; } 
        public int? PermissionId { get; set; }
    }
}
