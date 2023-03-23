using Adic;
using Adic.Container;

namespace HiplayGame
{
    public class LoginCommandsBindings : IBindingsSetup
    {
        public void SetupBindings(IInjectionContainer container)
        {
            container.RegisterExtension<CommanderContainerExtension>();

        }
    }

}


