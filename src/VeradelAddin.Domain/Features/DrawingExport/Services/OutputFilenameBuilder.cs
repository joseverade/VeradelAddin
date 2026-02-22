using VeradelAddin.Domain.Features.DrawingExport.Enum;
using VeradelAddin.Domain.Features.DrawingExport.Policies;

using System;
using System.Security.Policy;
namespace VeradelAddin.Domain.Features.DrawingExport.Services
{
    public class OutputFilenameBuilder
    {
        public static string Build(string Docname, ExportFormatType ext)
        {

            string extension = string.Empty;

            switch (ext)
            {
                case ExportFormatType.PDF:
                    extension = ".PDF";
                    break;
                case ExportFormatType.DWG:
                    extension = ".DWG";
                    break;
                case ExportFormatType.STEP:
                    extension = ".STEP";
                    break;
            }

            string suffix = string.Empty;
            string FileName = string.Concat(Docname, suffix, extension);
            return FileName;
        }

    }
}
