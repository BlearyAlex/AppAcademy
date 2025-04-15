using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.AbonosAcademy.Commands.CreateAbonoAcademy
{
    public class CreateAbonoAcademyResult
    {
        public int AbonoAcademyId { get; set; }
        public byte[] PdfBlob { get; set; }
    }
}
