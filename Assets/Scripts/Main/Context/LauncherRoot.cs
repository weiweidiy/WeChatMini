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

            //ͨ����,ûʲô������
            container.Bind<IAssetLoader>().ToSingleton<AddressableLoader>();
            container.Bind<ITransitionProvider>().ToSingleton<DefaultTransitionProvider>();
            container.Bind<IUIManager>().ToSingleton<UIManager>();
            
            //�����࣬ûʲô������
            container.Bind<IDataGridGenerater>().ToSingleton<DataGridGenerater>();
            container.Bind<IDataGridGenerater>().ToSingleton<DataGridGenerater2>();

            //�����࣬����һЩ����
            container.Bind<MapsManager>().ToSingleton<MapsManager>();

            //ҵ�񳡾���
            container.Bind<ISceneProvider>().ToSingleton<DefaultSceneProvider>(); //����uimanager? Ҫ�������
            container.Bind<IScenesManager>().ToSingleton<ScenesManager>();

            //����
            container.RegisterCommand<SwitchSceneCommand>();


            dispatcher = container.GetCommandDispatcher();
        }

        public override void Init()
        {
            //������������
            dispatcher.Dispatch<SwitchSceneCommand>("Login");

            
        }
    }

}


