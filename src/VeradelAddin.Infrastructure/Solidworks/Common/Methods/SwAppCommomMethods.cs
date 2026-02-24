using SolidWorks.Interop.swconst;
using System;
using System.IO;
using VeradelAddin.Infrastructure.Solidworks.Common.Exceptions;

namespace VeradelAddin.Infrastructure.Solidworks.Common.Methods
{
    public static class SwAppCommomMethods
    {

        public static swDocumentTypes_e GetDocumentTypeBasedOnPath(string FilePath)
        {
            string ext = Path.GetExtension(FilePath);

            if (ext == null) throw new ArgumentNullException("FilePath is null");

            switch (ext.ToLower())
            {
                case ".sldprt":
                    return swDocumentTypes_e.swDocPART;
                case ".sldasm":
                    return swDocumentTypes_e.swDocASSEMBLY;
                default:
                    throw new IncorrectExtensionException("The FilePath does not have an extension");
            }

        }

        

    }
}
