using Adic;
using Adic.Container;
using Cysharp.Threading.Tasks;

namespace HiplayGame
{
    public abstract class BaseBussiness : IGameBussiness
    {
        [Inject]
        protected IInjectionContainer _container;

        public abstract UniTask Run();
    }

}
