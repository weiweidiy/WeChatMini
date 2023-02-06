using Adic;
using Adic.Container;

namespace hiplaygame
{
    public class LoginCommandsBindings : IBindingsSetup
    {
        public void SetupBindings(IInjectionContainer container)
        {
            container.RegisterExtension<CommanderContainerExtension>()
                    .RegisterCommand<StartupLoginCommand>();
        }
    }

}


