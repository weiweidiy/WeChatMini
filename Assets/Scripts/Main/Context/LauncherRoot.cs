using Adic;

namespace hiplaygame
{




    public class LauncherRoot : ContextRoot
    {

        protected ICommandDispatcher dispatcher;

        public override void SetupContainers()
        {
            var container = this.AddContainer(new InjectionContainer(StaticReflectionCache.cache))
                        .RegisterExtension<CommanderContainerExtension>()
                        .RegisterCommand<SwitchSceneCommand>();

            container.Bind<IAssetLoader>().ToSingleton<AddressableLoader>();

            dispatcher = container.GetCommandDispatcher();
        }

        public override void Init()
        {
            //∑¢ÀÕ∆Ù∂Ø√¸¡Ó
            dispatcher.Dispatch<SwitchSceneCommand>("Login");
        }
    }



}


