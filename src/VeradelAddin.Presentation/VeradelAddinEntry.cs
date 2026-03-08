using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swpublished;
using System;
using System.Runtime.InteropServices;
using VeradelAddin.Infrastructure.FileSystem;
using VeradelAddin.Infrastructure.Solidworks.DrawingDocService;
using VeradelAddin.Presentation.AddinRibbon.SwUtilies;

namespace VeradelAddin.Presentation
{

    [ComVisible(true)]
    [Guid("C8F26576-7712-4925-A28A-CA7EB31D4FC3")]
    public sealed class VeradelAddinEntry : SwAddin
    {
        private static SldWorks SwApp;
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
            CommandManagerMediator mediator = new CommandManagerMediator();

            // CommandManager Wrapper
            Manager = new CommandManagerWrapper(_cookie, SwApp, mediator);

            CustomCommandsBuilder commandBuilder = new CustomCommandsBuilder(mediator, SwApp);

            commandBuilder.Build();

            Manager.ActivateCommandManager();

            return true;
        }



        #region Callback and Enable Methods
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

        #endregion

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
