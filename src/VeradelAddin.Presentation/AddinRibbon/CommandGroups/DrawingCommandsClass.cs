using SolidWorks.Interop.sldworks;
using System.Collections.Generic;
using VeradelAddin.Application.Common.Drawing.Enums;
using VeradelAddin.Application.Common.FileSystem.Contracts;
using VeradelAddin.Application.Common.FileSystem.DTOs;
using VeradelAddin.Application.Features.DrawingExport.UseCases;
using VeradelAddin.Domain.Enums.DrawingEnums;
using VeradelAddin.Infrastructure.FileSystem;
using VeradelAddin.Infrastructure.Solidworks.Common.Dto;
using VeradelAddin.Infrastructure.Solidworks.DrawingDocService;
using VeradelAddin.Presentation.AddinRibbon.SwUtilies;

namespace VeradelAddin.Presentation.AddinRibbon.CommandGroups.Drawing
{
    public class DrawingCommandsClass
    {


        public CommandItemWrapper GetFirstCommand(SldWorks solid)
        {

            DrawingDocService solidService = new DrawingDocService(solid, 3);
            IFileSystemServices fileService = new FileSystemService();

            ICollection<DrawingExportEnum> exportFormatEnums = new List<DrawingExportEnum>(3)
            {
                DrawingExportEnum.DWG,
                DrawingExportEnum.PDF,
                DrawingExportEnum.STEP
            };

            DirectoryDTO directoryDTO = new DirectoryDTO() { DirectoryName = "OBSOLETOS" };


            DrawingExportUseCase exportCase = new DrawingExportUseCase(
                solidService,
                fileService,
                new CreateDrawingDtos().GetConvertDrawingDTO(solid),
                exportFormatEnums,
                directoryDTO
              );


            CommandItemWrapper primerComando = new CommandItemWrapper(
                "ExportFile",
                "HintString",
                "Tooltip",
                exportCase.ExecuteCommand,
                exportCase.EnableMethod
                );

            return primerComando;
        }

    }


}
