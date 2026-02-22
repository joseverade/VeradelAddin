using SolidWorks.Interop.sldworks;
using System;
namespace VeradelAddin.Presentation.AddinRibbon
{
    public class CommandGroupWrapper
    {
        public enum Position
        {
            First,
            Last
        }

        public CommandGroupWrapper(string title, string toolTip, string hint)
        {
            Title = title;
            ToolTip = toolTip;
            Hint = hint;
        }

        public string Title { get; private set; }
        public string ToolTip { get; private set; }
        public string Hint { get; private set; }
        public CommandGroup SwGroup { get; set; }

        internal void AddCommand()
        {


        }
    }
}
