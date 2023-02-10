using Adic;
using Adic.Container;

namespace HiplayGame
{
    public class GameClassBindings : IBindingsSetup
    {
        public void SetupBindings(IInjectionContainer container)
        {
            container.Bind<IDataGridGenerater>().ToSingleton<DataGridGenerater>();
            container.Bind<IDataGridGenerater>().ToSingleton<DataGridGenerater2>();

            container.Bind<MapsManager>().ToSingleton<MapsManager>();
            

        }
    }
}


