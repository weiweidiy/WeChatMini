using Adic;
using Adic.Container;
using UnityEngine;


namespace hiplaygame
{
    public class StartupLoginCommand : Command
    {
        [Inject]
        IAssetLoader resourcesManager;

        [Inject]
        IInjectionContainer container;

        [Inject]
        IUIManager uiManager;

        [Inject("Canvas")]
        Transform parent;

        public override async void Execute(params object[] parameters)
        {
            Debug.Log("StartupMenuCommand");

            //创建开始按钮
            //var go = await uiManager.OpenUIAsync("StartButton", parent);
            var go = await uiManager.OpenUIAsync("StartButton", parent);
            container.Bind<StartButton>().ToGameObject(go).AsObjectName();
            container.Bind<MoveButton>().ToGameObject(go);
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


