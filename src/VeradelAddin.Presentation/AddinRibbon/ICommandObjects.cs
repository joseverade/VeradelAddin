using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeradelAddin.Presentation.AddinRibbon
{
    public interface ICommandObjects : IDisposable
    {
        void SetUserID(int userID);
    }
}
