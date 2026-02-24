using System;
using System.Collections.Generic;
using System.IO;
using VeradelAddin.Application.Common.Drawing.Contracts;
using VeradelAddin.Application.Common.Drawing.DrawingDTOs;
using VeradelAddin.Application.Common.FileSystem.Contracts;
using VeradelAddin.Application.Common.FileSystem.DTOs;
using VeradelAddin.Domain.Enums.DrawingEnums;
using VeradelAddin.Domain.Policies.DrawingPolicies.DrawingExportFormat;

namespace VeradelAddin.Application.Features.DrawingExport.UseCases
{


    /// <summary>
    /// S Solid principle (Single responsability), the class only coordinates the exporting of new files (SW)
    /// </summary>
    public sealed class DrawingExportUseCase
    {

        private readonly ISolidworksDrawing _swAppDrawingService;
        private readonly ICollection<DrawingExportEnum> _exportFormats;
        private readonly IFileSystemServices _fileService;

        private ConvertDrawingDTO _drawingDTO;
        private FolderDTO _oldFileName;

        public DrawingExportUseCase(ISolidworksDrawing swAppDrawing, IFileSystemServices fileService,
            ConvertDrawingDTO drawingDTO, ICollection<DrawingExportEnum> exportFormats, FolderDTO oldFileName)
        {
            _swAppDrawingService = swAppDrawing;
            _fileService = fileService;
            _drawingDTO = drawingDTO;
            _exportFormats = exportFormats;
            _oldFileName = oldFileName;
        }


        // 1. Obtener los formatos de conversion PDF DWG y STEP Listo
        // 2. Obtener el nombre del archivo Listo
        // 3. Verificar si el archivo lleva revision Listo
        // 4. Verificar si ya hay una version anterior (STEP, DWG, STEP)
        // 5. Si hay una version anterior, verificar si hay una carpeta obsoletos
        // 6. Si está el mismo archivo reemplazarlo
        // 7. Convertir a PDF, DWG y STEP
        // 8. Preguntar si desea abrir la carpeta
        public void ExecuteCommand()
        {
            _drawingDTO.Revision = _swAppDrawingService.GetRevionNumerTable();

            OlderVersionExists();

            string tempFolderPath = _fileService.CreateTempFolder();

            foreach (DrawingExportEnum format in _exportFormats)
            {
                string exportFileName = DrawingExportPolicy.SetDrawingName(_drawingDTO.Filename, _drawingDTO.Revision, format);

                if (!_fileService.TryDeleteFile(exportFileName)) throw new IOException("No se pudo eliminar el archivo, está en uso");
                ExportDrawing(tempFolderPath, exportFileName, format);   
            }

            _fileService.CopyAllFilesFromFolder(tempFolderPath, _drawingDTO.Path);
        }

        /// <summary>
        /// Checks whether an old version of the drawing to convert exists, and moves their files to an "obsolete" folder
        /// </summary>
        private void OlderVersionExists()
        {
            string olderVersionFilename = DrawingExportPolicy.GetOlderVesionFileWithoutExtension(_drawingDTO.Filename, _drawingDTO.Revision);

            if (_fileService.CheckOldVersion(olderVersionFilename))
            {
                _fileService.CreateFolder(_drawingDTO.Path, _oldFileName.FolderName);

                foreach (DrawingExportEnum ext in _exportFormats)
                {
                    string extension = DrawingExportPolicy.GetExtension(ext);
                    _fileService.MoveFile(_drawingDTO.Path, Path.Combine(_drawingDTO.Path, _oldFileName.FolderName),
                        olderVersionFilename, extension);
                }
            }
        }


        /// <summary>
        /// Creates documents and exports to a temp folder
        /// </summary>
        /// <param name="pathTempFolder"></param>
        /// <param name="format"></param>
        private void ExportDrawing(string pathTempFolder, string exportFileName , DrawingExportEnum format)
        {

            switch (format)
            {
                case DrawingExportEnum.PDF:
                    _swAppDrawingService.SaveAsPDF(pathTempFolder, exportFileName);
                    break;
                case DrawingExportEnum.DWG:
                    _swAppDrawingService.SaveAsDWG(pathTempFolder, exportFileName);
                    break;
                case DrawingExportEnum.STEP:
                    _swAppDrawingService.SaveAsSTEP(pathTempFolder, exportFileName);
                    break;
            }
        }

    }
}
