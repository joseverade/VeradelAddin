using SolidWorks.Interop.sldworks;
using System.IO;
using VeradelAddin.Application.Common.Drawing.DrawingDTOs;

namespace VeradelAddin.Infrastructure.Solidworks.Common.Dto
{
    public class CreateDrawingDtos
    {
        public ConvertDrawingDTO GetConvertDrawingDTO(SldWorks solid)
        {
            ConvertDrawingDTO dto = new ConvertDrawingDTO();

            string filename = ((ModelDoc2)solid.ActiveDoc).GetPathName();
            
            dto.Path = filename;
            dto.Filename = Path.GetFileName(filename);
            return dto;
        }


    }
}
