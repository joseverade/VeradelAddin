using System;
using System.Collections;
using System.Drawing.Drawing2D;
using System.Security.Policy;
using VeradelAddin.Domain.Enums.DrawingEnums;

namespace VeradelAddin.Domain.Policies.DrawingPolicies.DrawingExportFormat
{
    public class DrawingExportPolicy
    {
        public static string SetDrawingName(string filenameWithoutExtension, int revisionNumber, DrawingExportEnum ext)
        {

            string newFileName = JoinFileNameAndRevisionNumber(filenameWithoutExtension, revisionNumber);

            string extension = GetExtension(ext);

            return newFileName + extension;
        }

        public static string GetOlderVesionFileWithoutExtension(string filenameWithoutExtension, int revisionNumber)
        {
            revisionNumber = revisionNumber - 1;
            return JoinFileNameAndRevisionNumber(filenameWithoutExtension, revisionNumber);
        }

        public static string GetExtension(DrawingExportEnum ext)
        {
            string extension = string.Empty;
            switch (ext)
            {
                case DrawingExportEnum.PDF:
                    extension = ".PDF";
                    break;
                case DrawingExportEnum.DWG:
                    extension = ".DWG";
                    break;
                case DrawingExportEnum.STEP:
                    extension = ".STEP";
                    break;
            }

            return extension;
        }

        private static string JoinFileNameAndRevisionNumber(string filenameWithoutExtension, int revisionNumber)
        {
            string revisionNumberString = string.Empty;
            if (0 < revisionNumber && revisionNumber < 10)
                revisionNumberString = $"_R0{revisionNumber}";
            else if (10 < revisionNumber)
                revisionNumberString = $"R{revisionNumber}";

            return filenameWithoutExtension + revisionNumber;
        }


    }
}
