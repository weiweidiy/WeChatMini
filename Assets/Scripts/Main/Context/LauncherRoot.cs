using Adic;

namespace HiplayGame
{




    public class LauncherRoot : ContextRoot
    {

        protected ICommandDispatcher dispatcher;

        public override void SetupContainers()
        {
            DontDestroyOnLoad(this);
            //var globalContainer = this.AddContainer(new InjectionContainer(StaticReflectionCache.cache), false);
            //globalContainer.Bind<LocalDataManager>().ToSingleton();


            var container = this.AddContainer(new InjectionContainer(StaticReflectionCache.cache))
                        .RegisterExtension<CommanderContainerExtension>();

            //通用类,没什么依赖的
            container.Bind<IAssetLoader>().ToSingleton<AddressableLoader>();
            container.Bind<ITransitionProvider>().ToSingleton<DefaultTransitionProvider>();
            container.Bind<IUIManager>().ToSingleton<UIManager>();
            
            //帮助类，没什么依赖的
            container.Bind<IDataGridGenerater>().ToSingleton<DataGridGenerater>();
            container.Bind<IDataGridGenerater>().ToSingleton<DataGridGenerater2>();

            //管理类，依赖一些子类
            container.Bind<MapsManager>().ToSingleton<MapsManager>();

            //业务场景类
            container.Bind<ISceneProvider>().ToSingleton<DefaultSceneProvider>(); //依赖uimanager? 要放在最后
            container.Bind<IScenesManager>().ToSingleton<ScenesManager>();

            //命令
            container.RegisterCommand<SwitchSceneCommand>();


            dispatcher = container.GetCommandDispatcher();
        }

        public override void Init()
        {
            //发送启动命令
            dispatcher.Dispatch<SwitchSceneCommand>("Login");

            
        }
    }

}


