using Adic;
using Adic.Container;
using Adic.Injection;

namespace HiplayGame
{
    /// <summary>
    /// 用 toself 的方式绑定类型 的泛型工厂
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PlaceHolderFactory<T> : IFactory
    {
        IInjectionContainer container;
        public PlaceHolderFactory(IInjectionContainer container)
        {
            this.container = container;

            container.Bind<T>().ToSingleton();
        }

        public object Create(InjectionContext context)
        {
            return this.container.Resolve<T>();
        }
    }
}
