using System.Collections.Generic;
using VeradelAddin.Application.Features.DrawingExport.Contracts;
using VeradelAddin.Domain.Features.DrawingExport.Enum;
using VeradelAddin.Domain.Features.DrawingExport.Services;

namespace VeradelAddin.Application.Features.DrawingExport.UseCases
{



    public sealed class DrawingExportUseCase
    {

        private ISwConverterServices _swConverterServices;
        private IFileSystemServices _fileSystemServices;

        public DrawingExportUseCase(ISwConverterServices swConverterServices, IFileSystemServices fileSystemServices)
        {
            _swConverterServices = swConverterServices;
            _fileSystemServices = fileSystemServices;
        }

        public bool ExecuteCommand(IReadOnlyCollection<ExportFormatType> formatos, string PathName)
        {
            int nRevision = _swConverterServices.GetNumberRowRevisionList();

            foreach (ExportFormatType formato in formatos)
            {
                string Filename = OutputFilenameBuilder.Build(PathName, formato);

                switch (formato)
                {
                    case ExportFormatType.PDF:
                        _swConverterServices.ConvertDrawingToPDF(Filename);
                        break;
                    case ExportFormatType.DWG:
                        _swConverterServices.ConvertDrawingToDWG(Filename);
                        break;
                    case ExportFormatType.STEP:
                        _swConverterServices.ConvertDrawingToSTEP(Filename);
                        break;
                }
            }

            return true;

        }

    }
}
