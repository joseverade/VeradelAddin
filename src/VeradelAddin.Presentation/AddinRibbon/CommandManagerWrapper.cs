using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Proxies;
using System.Xml;

namespace VeradelAddin.Presentation.AddinRibbon
{
    public sealed class CommandManagerWrapper : IDisposable
    {

        private readonly int _cookie;

        private readonly SldWorks _swApp;

        private readonly CommandManagerMediator _mediator;


        public CommandManagerWrapper(int cookie, SldWorks swApp, CommandManagerMediator mediator)
        {
            _cookie = cookie;
            _swApp = swApp;
            _mediator = mediator;
            CreateCommandManagerInstance();
        }

        private void CreateCommandManagerInstance() => _mediator.CreateCommanManager(_swApp.GetCommandManager(_cookie));

        public void ActivateCommandManager() => _mediator.Activate();

        public void ExecuteMethod(int index) => _mediator.ExecuteCommand(index);

        public void Dispose() => _mediator.Dispose();
    }
}
