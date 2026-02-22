using SolidWorks.Interop.sldworks;
using System.Collections.Generic;
using System.Runtime.Remoting.Proxies;

namespace VeradelAddin.Presentation.AddinRibbon
{
    public sealed class CommandManagerWrapper
    {
        private readonly int _cookie;
        private readonly SldWorks _swApp;
        public CommandManager SwCommandManager { get; private set; }
        private List<CommandGroupWrapper> commandGroupsWrappers;
        private List<CommandWrapper> commandWrappersList;



        public CommandManagerWrapper(int cookie, SldWorks swApp)
        {
            _cookie = cookie;
            _swApp = swApp;
            commandGroupsWrappers = new List<CommandGroupWrapper>();
            commandWrappersList = new List<CommandWrapper>();
        }


        private void CreateRibbonAndCommands()
        {
            SwCommandManager = _swApp.GetCommandManager(_cookie);

            
        }


        public CommandGroupWrapper AddCommandGroup(string title, string toolTip, string hint)
        {
            int errors = 0;

            CommandGroupWrapper newGroup =  new CommandGroupWrapper(title, toolTip, hint);

            commandGroupsWrappers.Add(newGroup);

            CommandGroup newCommandGroup = SwCommandManager.CreateCommandGroup2(commandGroupsWrappers.Count,
                newGroup.Title, newGroup.ToolTip, newGroup.Hint, 1, true, errors);

            newGroup.SwGroup = newCommandGroup;

            return newGroup;
        }

        public void ActivateCommandManager()
        {

        }

    }
}
