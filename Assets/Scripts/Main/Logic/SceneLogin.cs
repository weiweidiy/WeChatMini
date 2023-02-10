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
            _container.Bind<BussinessInitializeLoginSceneUI>().ToSelf();
        }

        public async override UniTask OnEnter()
        {
            var logic = _container.Resolve<BussinessInitializeLoginSceneUI>();
            await logic.Run();
        }
    }
}
