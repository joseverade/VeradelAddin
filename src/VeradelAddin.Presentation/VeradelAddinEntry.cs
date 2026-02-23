 using System;
using SolidWorks.Interop.swpublished;
using SolidWorks.Interop.sldworks;
using System.Dynamic;
using VeradelAddin.Presentation.AddinRibbon;
using System.Runtime.InteropServices;

namespace VeradelAddin.Presentation
{

    [ComVisible(true)]
    [Guid("C8F26576-7712-4925-A28A-CA7EB31D4FC3")]
    public sealed class VeradelAddinEntry : SwAddin
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

            Manager.Dispose();

            SwApp = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            return true;
        }

        private bool CreateAddinRibbon()
        {

            // Mediator class, it handles the communication between
            // CommandManager, CommandGroups and CommandItems
            CommandManagerMediator commandManagerMediator = new CommandManagerMediator();

            // CommandManager Wrapper
            Manager = new CommandManagerWrapper(_cookie, SwApp, commandManagerMediator);

            // Creation of group 1
            CommandGroupWrapper group1 = new CommandGroupWrapper(
                "Veradel Grupo 1",
                "Mejorar",
                "Fluidez",
                "ToolbarLarge.bmp",
                false,
                false,
                true,
                commandManagerMediator);

            // new CommandItem
            group1.AddCommandItem(new CommandItemWrapper(
                "Conversor de archivos",
                "Convierte archivos a diferentes formatos",
                "Convierte archivos",
                Foo,
                EnableFoo));
                //));



            Manager.ActivateCommandManager();

            return true;
        }


        public void CallbackFunction(string args)
        {
            int index;

            bool ok = int.TryParse(args, out index);
            if (!ok) return;

            Manager.ExecuteMethod(index);
        }

        public int EnableMethod(string args)
        {

            return 1;
        }


        public void Foo()
        {
             SwApp.SendMsgToUser2("ABC",1,1);
        }

        public void Foo2()
        {
            SwApp.SendMsgToUser2("CDF", 1, 1);
        }

        public int EnableFoo()
        {
            return 1;
        }




        #region Functions for registry
 
        [ComRegisterFunction()]
        public static void Register(Type t)
        {
            try
            {
                Microsoft.Win32.RegistryKey clave = Microsoft.Win32.Registry.LocalMachine.CreateSubKey($@"SOFTWARE\SolidWorks\Addins\{t.GUID.ToString("B")}");
                clave.SetValue(null, 0);
                clave.SetValue("Description", "Prohibido la comercializacion");
                clave.SetValue("Title", "VeradelAddin");
                clave.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        [ComUnregisterFunction()]
        public static void Unregister(Type t)
        {
            try
            {
                Microsoft.Win32.Registry.LocalMachine
                    .DeleteSubKey(@"SOFTWARE\SolidWorks\Addins\" + t.GUID.ToString("B"), false);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

        }
        #endregion

    }
}
