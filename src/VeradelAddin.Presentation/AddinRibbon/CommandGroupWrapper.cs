using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.IO;
using System.Net;
using System.Reflection;
namespace VeradelAddin.Presentation.AddinRibbon
{
    public class CommandGroupWrapper : ICommandObjects
    {
        public string Title { get; private set; }
        public string ToolTip { get; private set; }
        public string Hint { get; private set; }
        public int Errors { get; private set; }
        public string IconFilename { get; private set; }
        public CommandGroup commandGroup { get; private set; }
        public int UserID { get; private set; }
        public int ShowInDocumentType { get; private set; }
        public string[] IconList { get; private set; }

        private CommandManagerMediator _mediator;


        public CommandGroupWrapper(string title, string toolTip, string hint,
            string iconFileName, bool showPrt, bool showAsm, bool showDrw, CommandManagerMediator mediator)
        {
            Title = title;
            ToolTip = toolTip;
            Hint = hint;
            ShowInDocumentType = ((showPrt ? 1 : 0) << 1) | (showAsm ? 1 : 0) << 2 | (showDrw ? 1 : 0) << 3;
            IconFilename = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), iconFileName);
            _mediator = mediator;

            IconList = new string[1];
            IconList[0] = iconFileName;

            CreateGroup();
        }


        private void CreateGroup() => _mediator.CreateCommandGroup(this);
        public void SetUserID(int userID) => UserID = userID;
        public void OnGroupCreated(int errors, CommandGroup group)
        {
            commandGroup = group;
            Errors = errors;
        }


        public void AddCommandItem(CommandItemWrapper newCommand) => _mediator.AddCommandItem(newCommand, this);


        public void Activate() => commandGroup.Activate();


        public void Dispose()
        {
            commandGroup = null;
        }

    }
}
