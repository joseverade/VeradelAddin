using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeradelAddin.Domain.Features.DrawingExport.Enum
{
    [Flags]
    public enum ExportFormatType
    {
        PDF,
        DWG,
        STEP
    }

}
