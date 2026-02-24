using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeradelAddin.Infrastructure.Solidworks.Init
{
    public class SolidWorksInitializer
    {

        public SldWorks SwApp { get; }

        public SolidWorksInitializer(SldWorks SwApp)
        {
            this.SwApp = SwApp;
        }



    }
}
