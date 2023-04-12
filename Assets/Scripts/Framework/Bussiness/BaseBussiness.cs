using Adic;
using Adic.Container;
using Cysharp.Threading.Tasks;

namespace HiplayGame
{
    public abstract class BaseBusiness : IGameBusiness
    {
        [Inject]
        protected IInjectionContainer _container;

        [Inject]
        protected virtual void Initialize() { }

        public abstract UniTask Run();
    }

}
