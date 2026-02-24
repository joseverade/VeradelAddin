using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeradelAddin.Infrastructure.Solidworks.Common.Exceptions
{
    public class IncorrectExtensionException : Exception
    {
        public IncorrectExtensionException(string message) : base(message)
        {
        }
    }
}
