using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace VeradelAddin.Infrastructure.Solidworks.DrawingDocService.Exceptions
{

    [Serializable]
    public class NotDrawingException : Exception
    {
        public NotDrawingException(string message) : base(message)
        {

        }
    }

    [Serializable]
    internal class NoSheetAvaliableException : Exception
    {
        public NoSheetAvaliableException(string message) : base(message)
        {
        }

    }

    [Serializable]
    internal class NoReferencedDocumentFoundException : Exception
    {

        public NoReferencedDocumentFoundException(string message) : base(message)
        {
        }

    }
}
