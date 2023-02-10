using Adic;
using Adic.Container;
using Adic.Injection;

namespace HiplayGame
{
    /// <summary>
    /// �� toself �ķ�ʽ������ �ķ��͹���
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
