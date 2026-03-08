namespace VeradelAddin.Application.Common.Drawing.Contracts
{
    public interface ISolidworksDrawing
    {
        int GetRevionNumerTable();
        void SaveAsPDF(string pathTempDirectory, string exportFileName);
        void SaveAsDWG(string pathTempDirectory, string exportFileName);
        void SaveAsSTEP(string pathTempDirectory, string exportFileName);
    }
}