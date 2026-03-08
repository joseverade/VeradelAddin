using System;

namespace VeradelAddin.Presentation.AddinRibbon.SwUtilies
{
    public class CommandItemWrapper : ICommandObjects
    {

        public string Name { get; private set; }
        public string HintString { get; private set; }
        public string ToolTip { get; private set; }
        public int UserID { get; private set; }
        public int CommandIndex {  get; private set; }
        public Action CallbackFunction { get; private set; }
        public Func<int> EnableMethod { get; set; }
    
        public CommandItemWrapper(string name, string hintString, string tooltip,
            Action callbackFunction, Func<int> enableMethod)
        {
            Name = name;
            HintString = hintString;   
            ToolTip = tooltip;
            CallbackFunction = callbackFunction;
            EnableMethod = enableMethod;
        }
        public void SetUserID(int userID) => UserID = userID;
        public void OnCommandCreated(int commandIndex) => CommandIndex = commandIndex;

        public void Dispose()
        {
            CallbackFunction = null;
            EnableMethod = null;
        }
    }
}
