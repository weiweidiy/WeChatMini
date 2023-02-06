using Adic;
using Adic.Container;

namespace hiplaygame
{
    public class CommonCommandsBindings : IBindingsSetup
    {
        public void SetupBindings(IInjectionContainer container)
        {
            container.RegisterExtension<CommanderContainerExtension>()
                    .RegisterCommand<SwitchSceneCommand>();
        }
    }
}


