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




        }


    }
}
