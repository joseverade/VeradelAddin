using VeradelAddin.Domain.Common;
using VeradelAddin.Domain.DTOs;

namespace VeradelAddin.Domain.DrawingUseCases.ExportDrawingFormat
{
    public sealed class ExportDrawing
    {

        private ISolidworksDrawingInterface _swAppDrawing;

        public ExportDrawing(ISolidworksDrawingInterface swAppDrawing, DrawingDTO drawing)
        {
            _swAppDrawing = swAppDrawing;
        }
        public void ExecuteCommand()
        {
            // 1. Obtener los formatos de conversion PDF DWG y STEP
            // 2. 



        }


    }
}
