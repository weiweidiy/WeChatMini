using System;
using Adic;
using Adic.Container;
using Cysharp.Threading.Tasks;

namespace HiplayGame
{
    public abstract class BaseScene : IScene
    {

        [Inject]
        protected IInjectionContainer _container;

        public virtual string Name => GetType().ToString();

        public virtual string Location => GetType().ToString();   

        [Inject]
        public virtual void Initialize() { }

        public async virtual UniTask OnEnter() { await UniTask.DelayFrame(0); }

        public virtual void OnEnterTransitionComplete() { }

        public virtual void OnExit() { }

        public virtual void OnExitTransitionStart() { }
    }
}
