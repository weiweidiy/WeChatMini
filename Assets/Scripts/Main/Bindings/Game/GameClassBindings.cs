using Adic;
using Adic.Container;

namespace HiplayGame
{
    public class GameClassBindings : IBindingsSetup
    {
        public void SetupBindings(IInjectionContainer container)
        {
            container.Bind<DataGridGenerater>().ToSingleton<DataGridGenerater>();
            container.Bind<MapsManager>().ToSingleton<MapsManager>();
            
        }
    }
}


