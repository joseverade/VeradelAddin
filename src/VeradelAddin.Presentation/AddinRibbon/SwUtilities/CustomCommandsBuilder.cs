using SolidWorks.Interop.sldworks;
using VeradelAddin.Presentation.AddinRibbon.CommandGroups.Drawing;

namespace VeradelAddin.Presentation.AddinRibbon.SwUtilies
{

    public class CustomCommandsBuilder
    {
        private readonly CommandManagerMediator _mediator;
        private readonly SldWorks _swapp;

        public CustomCommandsBuilder(CommandManagerMediator mediator, SldWorks swapp)
        {
            _mediator = mediator;
            _swapp = swapp;
        }

        /// <summary>
        /// Here is the place to create groups and commands
        /// </summary>
        public void Build()
        {

            DrawingCommands();
            AssemblyCommands();
            PartCommands();
        }


        private void DrawingCommands()
        {
            // First group 
            CommandGroupWrapper group1 = new CommandGroupWrapper("Comandos dibujo", "ToolTipoComandosDibujo", "HintComandosDibujo", string.Empty,
                false, false, true, _mediator);

            group1.AddCommandItem(new DrawingCommandsClass().GetFirstCommand(_swapp));


        }


        private void AssemblyCommands()
        {

        }

        private void PartCommands()
        {

        }


    }
}
