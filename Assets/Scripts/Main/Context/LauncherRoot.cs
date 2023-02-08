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
                        //通用模块管理器绑定
                        .SetupBindings<CommonClassBindings>()
                        .RegisterCommand<SwitchSceneCommand>();

            //container.Bind<IAssetLoader>().ToSingleton<AddressableLoader>();

            dispatcher = container.GetCommandDispatcher();
        }

        public override void Init()
        {
            //发送启动命令
            dispatcher.Dispatch<SwitchSceneCommand>("Login");
        }
    }



}


