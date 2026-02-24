namespace VeradelAddin.Application.Common.Drawing.Contracts
{
    public interface ISolidworksDrawing
    {
        int GetRevionNumerTable();
        void SaveAsPDF(string pathTempFolder, string exportFileName);
        void SaveAsDWG(string pathTempFolder, string exportFileName);
        void SaveAsSTEP(string pathTempFolder, string exportFileName);
    }
}