using Adic;
using Adic.Container;
using UnityEngine;

namespace HiplayGame
{
    public class SceneGame : BaseScene
    {
        IInjectionContainer container;

        public SceneGame(IInjectionContainer container)
        {
            this.container = container;

            this.container.Bind<BussinessInitializeGameSceneUI>().ToSelf();
        }

        public override string Location => "Game";

        public override void OnEnter()
        {
            Debug.Log("SceneGame OnEnter");

            var logic = container.Resolve<BussinessInitializeGameSceneUI>();
            logic.Run();

        }

    }

}
