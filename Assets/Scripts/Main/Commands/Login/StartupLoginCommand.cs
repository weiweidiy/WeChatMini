using Adic;
using Adic.Container;
using UnityEngine;


namespace HiplayGame
{
    public class StartupLoginCommand : Command
    {
        [Inject]
        IInjectionContainer container;

        [Inject]
        IUIManager uiManager;

        [Inject("Canvas")] //场景上的Canvas
        Transform parent;

        public override async void Execute(params object[] parameters)
        {
            Debug.Log("StartupMenuCommand");

            //创建开始按钮
            //var go = await uiManager.OpenUIAsync("StartButton", parent);
            var go = await uiManager.OpenUIAsync("StartButton", parent);
            container.Bind<StartButton>().ToGameObject(go).AsObjectName(); //attacth一个组件上去
            //container.Bind<MoveButton>().ToGameObject(go); //attacth一个组件上去
            var component = go.GetComponent<StartButton>();
            Debug.Assert(component != null, "component is null");

            component.onClicked += Component_onClicked;
        }

        private void Component_onClicked()
        {
            dispatcher.Dispatch<SwitchSceneCommand>("Game", "SMFadeTransition");
        }
    }
}


