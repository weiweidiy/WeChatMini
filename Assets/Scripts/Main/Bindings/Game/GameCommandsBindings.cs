using Adic;
using Adic.Container;

namespace HiplayGame
{
    public class GameCommandsBindings : IBindingsSetup
    {
        public void SetupBindings(IInjectionContainer container)
        {
            container.RegisterExtension<CommanderContainerExtension>();
                   //.RegisterCommand<StartupGameCommand>();
        }
    }

}


