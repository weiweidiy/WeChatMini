using Adic;
using Adic.Container;

namespace hiplaygame
{
    public class GameCommandsBindings : IBindingsSetup
    {
        public void SetupBindings(IInjectionContainer container)
        {
            container.RegisterExtension<CommanderContainerExtension>()
                   .RegisterCommand<StartupGameCommand>();
        }
    }

}


