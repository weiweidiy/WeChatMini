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

        [Inject("Canvas")] //�����ϵ�Canvas
        Transform parent;

        public override async void Execute(params object[] parameters)
        {
            Debug.Log("StartupMenuCommand");

            //������ʼ��ť
            //var go = await uiManager.OpenUIAsync("StartButton", parent);
            var go = await uiManager.OpenUIAsync("StartButton", parent);
            container.Bind<StartButton>().ToGameObject(go).AsObjectName(); //attacthһ�������ȥ
            //container.Bind<MoveButton>().ToGameObject(go); //attacthһ�������ȥ
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


