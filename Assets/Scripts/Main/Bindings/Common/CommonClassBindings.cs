using Adic;
using Adic.Container;

namespace HiplayGame
{
    public class CommonClassBindings : IBindingsSetup
    {
        public void SetupBindings(IInjectionContainer container)
        {
            container.Bind<IAssetLoader>().ToSingleton<AddressableLoader>();
            container.Bind<IUIManager>().ToSingleton<UIManager>();          
            container.Bind<ITransitionProvider>().ToSingleton<DefaultTransitionProvider>();
            container.Bind<ISceneProvider>().ToSingleton<DefaultSceneProvider>(); 
            container.Bind<IScenesManager>().ToSingleton<ScenesManager>();
        }
    }
}


