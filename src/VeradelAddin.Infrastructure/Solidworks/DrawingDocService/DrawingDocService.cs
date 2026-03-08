using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System.IO;
using VeradelAddin.Application.Common.Drawing.Contracts;
using VeradelAddin.Infrastructure.Solidworks.Common.Methods;
using VeradelAddin.Infrastructure.Solidworks.DrawingDocService.Exceptions;

namespace VeradelAddin.Infrastructure.Solidworks.DrawingDocService
{
    public class DrawingDocService : ISolidworksDrawing
    {

        private readonly SldWorks _swApp;
        private DrawingDoc _drawing;
        private int _rowOffset;

        // Common Objects

        private IAdvancedSaveAsOptions _advancedOption;
        private int _error = 0;
        private int _warnings = 0;

        public DrawingDocService(SldWorks swApp, int rowOffset)
        {
            _swApp = swApp;
            _rowOffset = rowOffset;
            InitializeCommonObjects();
        }

        private void InitializeCommonObjects()
        {
            _drawing = _swApp.ActiveDoc() as DrawingDoc;
            if (_drawing == null) throw new NotDrawingException("Active is not a drawing doc");

            _advancedOption = ((ModelDoc2)_drawing).Extension.GetAdvancedSaveAsOptions(
                (int)swSaveWithReferencesOptions_e.swSaveWithReferencesOptions_None);
        }


        public int GetRevionNumerTable()
        {
            
            string[] names = _drawing.GetSheetNames();
            if (names == null) throw new NoSheetAvaliableException("No name sheet found");


            RevisionTableAnnotation rTable = null;

            foreach (string name in names)
            {
                Sheet selectedSheet = _drawing.Sheet[name];
                if (selectedSheet == null) continue;

                if (selectedSheet.RevisionTable != null)
                {
                    rTable = selectedSheet.RevisionTable;
                    break;
                }

            }

            return ((TableAnnotation)rTable).RowCount - _rowOffset;
        }

        public void SaveAsPDF(string tempDirectoryPath, string exportFileName)
        {

            ExportPdfData exportData = _swApp.GetExportFileData((int)swExportDataFileType_e.swExportPdfData);
            exportData.ViewPdfAfterSaving = false;

            ((ModelDoc2)_drawing).Extension.SaveAs3(
                Path.Combine(tempDirectoryPath, exportFileName),
               (int)swSaveAsVersion_e.swSaveAsCurrentVersion,
               (int)swSaveAsOptions_e.swSaveAsOptions_Silent,
               exportData,
               _advancedOption, ref _error, ref _warnings);
        }

        public void SaveAsDWG(string tempDirectoryPath, string exportFileName)
        {
            ((ModelDoc2)_drawing).Extension.SaveAs3(
               Path.Combine(tempDirectoryPath, exportFileName),
              (int)swSaveAsVersion_e.swSaveAsCurrentVersion,
              (int)swSaveAsOptions_e.swSaveAsOptions_Silent,
              null,
              _advancedOption, ref _error, ref _warnings);
        }



        public void SaveAsSTEP(string tempDirectoryPath, string exportFileName)
        {

            Sheet activeSheet = _drawing.GetCurrentSheet();
            if (activeSheet == null) throw new NoSheetAvaliableException("No Active Sheet Found");

            object[] oViews = activeSheet.GetViews();

            ModelDoc2 referencedModel = null;

            foreach (View view in oViews)
            {
                if (view == null) continue;

                referencedModel = view.ReferencedDocument;
                break;

            }

            if (referencedModel == null) throw new NoReferencedDocumentFoundException(
                "The current sheet does not have a view with a referened document");


            string referencedName = referencedModel.GetPathName();


            int openDocErrors = 0;
            int openDocWarnings = 0;

            ModelDoc2 referencedDocument = _swApp.OpenDoc6(referencedName,
                (int)SwAppCommomMethods.GetDocumentTypeBasedOnPath(referencedName),
                (int)swOpenDocOptions_e.swOpenDocOptions_Silent,
                string.Empty,
                ref openDocErrors,
                ref openDocWarnings
                );


            if (openDocErrors != 0) throw new System.Exception($"Error al abrir el archivo codigo error: {openDocErrors}");

            _swApp.ActivateDoc3(referencedName, false, (int)swRebuildOnActivation_e.swUserDecision, ref openDocErrors);

            referencedDocument.Extension.SaveAs3(
                Path.Combine(tempDirectoryPath, exportFileName),
                (int)swSaveAsVersion_e.swSaveAsCurrentVersion,
                (int)swSaveAsOptions_e.swSaveAsOptions_Silent,
                null,
                _advancedOption,
                _error,
                _warnings);
            _swApp.CloseDoc(Path.GetFileName(referencedName));

        }



    }
}
