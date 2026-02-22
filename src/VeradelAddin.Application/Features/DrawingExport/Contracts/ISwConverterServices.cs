namespace VeradelAddin.Application.Features.DrawingExport.Contracts
{
    public interface ISwConverterServices
    {
        int GetNumberRowRevisionList();
        void ConvertDrawingToDWG(string filename);
        void ConvertDrawingToPDF(string filename);
        void ConvertDrawingToSTEP(string filename);
    }
}