using Adic;

namespace HiplayGame
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
            container.Bind<ITransitionProvider>().ToSingleton<DefaultTransitionProvider>();
            container.Bind<ISceneProvider>().ToSingleton<DefaultSceneProvider>();
            container.Bind<IScenesManager>().ToSingleton<ScenesManager>();


            dispatcher = container.GetCommandDispatcher();
        }

        public override void Init()
        {
            //∑¢ÀÕ∆Ù∂Ø√¸¡Ó
            dispatcher.Dispatch<SwitchSceneCommand>("Login");
        }
    }



}


