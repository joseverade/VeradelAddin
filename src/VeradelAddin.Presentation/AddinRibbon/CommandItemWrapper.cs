using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace VeradelAddin.Presentation.AddinRibbon
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


        //CommandManagerMediator _mediator;
    
        public CommandItemWrapper(string name, string hintString, string tooltip,
            Action callbackFunction, Func<int> enableMethod)
        {
            //_mediator = mediator;
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
