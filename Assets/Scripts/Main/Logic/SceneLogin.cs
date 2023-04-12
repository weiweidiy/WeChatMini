using System;
using Adic.Container;
using Cysharp.Threading.Tasks;

namespace HiplayGame
{
    public class SceneLogin : BaseScene
    {
        public override string Location => "Login";

        public override void Initialize()
        {
            _container.Bind<BusinessInitializeLoginSceneUI>().ToSelf();
        }

        public async override UniTask OnEnter()
        {
            await base.OnEnter();
            var logic = _container.Resolve<BusinessInitializeLoginSceneUI>();
            await logic.Run();
        }
    }
}
