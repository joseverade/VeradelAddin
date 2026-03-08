using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;

namespace VeradelAddin.Presentation.AddinRibbon.SwUtilies
{
    public class CommandManagerMediator : IDisposable
    {

        public List<ICommandObjects> CommandManagerObjects;
        private CommandManager commandManager;


        public CommandManagerMediator() => CommandManagerObjects = new List<ICommandObjects>();


        public void CreateCommanManager(CommandManager swCommandManager) => commandManager = swCommandManager;

        public void CreateCommandGroup(CommandGroupWrapper createdGroup)
        {
            int errors = 0;

            createdGroup.SetUserID(CommandManagerObjects.Count);
            CommandManagerObjects.Add(createdGroup);

            CommandGroup group = commandManager.CreateCommandGroup2(createdGroup.UserID,
                createdGroup.Title, createdGroup.ToolTip, createdGroup.Hint, -1, true, ref errors);

            group.ShowInDocumentType = createdGroup.ShowInDocumentType;
            group.IconList = createdGroup.IconList;
            group.MainIconList = createdGroup.IconList;

            createdGroup.OnGroupCreated(errors, group);
        }

        public void AddCommandItem(CommandItemWrapper newCommand, CommandGroupWrapper commandGroupWrapper)
        {
            newCommand.SetUserID(CommandManagerObjects.Count);
            CommandManagerObjects.Add(newCommand);

            int CommandIndex = commandGroupWrapper.commandGroup.AddCommandItem2(
                newCommand.Name, -1, newCommand.HintString, newCommand.ToolTip,
                newCommand.UserID, $"CallbackFunction({newCommand.UserID})",
                $"EnableMethod({newCommand.UserID})", newCommand.UserID, 3);

            newCommand.OnCommandCreated(CommandIndex);
        }


        public void Activate()
        {
            foreach (ICommandObjects obj in CommandManagerObjects)
            {
                if (obj.GetType() == typeof(CommandGroupWrapper))
                    ((CommandGroupWrapper)obj).Activate();
            }
        }

        public void ExecuteCommand(int index) => (CommandManagerObjects[index] as CommandItemWrapper)?.CallbackFunction.Invoke();

        public void Dispose()
        {
            foreach (ICommandObjects obj in CommandManagerObjects)
            {
                if (obj.GetType() == typeof(CommandGroupWrapper))
                {
                    commandManager.RemoveCommandGroup2(((CommandGroupWrapper)obj).UserID, false);
                }

                obj.Dispose();
            }
            CommandManagerObjects = null;
            commandManager = null;
        }


    }
}
