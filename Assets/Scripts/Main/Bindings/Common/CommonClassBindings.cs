using Adic;
using Adic.Container;

namespace hiplaygame
{
    public class CommonClassBindings : IBindingsSetup
    {
        public void SetupBindings(IInjectionContainer container)
        {
            container.Bind<IAssetLoader>().ToSingleton<AddressableLoader>();
            container.Bind<IUIManager>().ToSingleton<UIManager>();
            container.Bind<IScenesManager>().ToSingleton<ScenesManager>();
            container.Bind<ITransitionProvider>().ToSingleton<DefaultTransitionProvider>();
            container.Bind<ISceneProvider>().ToSingleton<DefaultSceneProvider>();
        }
    }
}


