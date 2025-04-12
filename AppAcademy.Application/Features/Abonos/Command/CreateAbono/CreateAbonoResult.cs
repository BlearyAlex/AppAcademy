using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Abonos.Command.CreateAbono
{
    public class CreateAbonoResult
    {
        public int AbonoId { get; set; }
        public byte[] PdfBlob { get; set; }
    }
}
