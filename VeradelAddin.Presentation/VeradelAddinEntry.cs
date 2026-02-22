using System;
using SolidWorks.Interop.swpublished;
using SolidWorks.Interop.sldworks;
using System.Dynamic;
using VeradelAddin.Presentation.AddinRibbon;

namespace VeradelAddin.Presentation
{
    public class VeradelAddinEntry : SwAddin
    {
        public static SldWorks SwApp;
        private static int _cookie;

        CommandManagerWrapper Manager;

        public bool ConnectToSW(object ThisSW, int Cookie)
        {
            _cookie = Cookie;
            SwApp = (SldWorks)ThisSW;

            SwApp.SetAddinCallbackInfo2(0, this, _cookie);


            CreateAddinRibbon();
            return true;
        }

        

        public bool DisconnectFromSW()
        {


            SwApp = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            return true;
        }

        private bool CreateAddinRibbon()
        {

            Manager = new CommandManagerWrapper(_cookie, SwApp);
            CommandGroupWrapper group1 =  Manager.AddCommandGroup("Comandos Dibujo Veradel", "Comandos de automatizacion", "Mejora la eficiencia");

            group1.AddCommand( new );

            Manager.ActivateCommandManager();

            return true;
        }


        private bool CallbackMethod(string args)
        {

            


        }



    }
}
