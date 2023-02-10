using Adic;
using Adic.Container;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace HiplayGame
{
    public class BussinessInitializeLoginSceneUI : IGameBussiness
    {
        [Inject]
        IUIManager uiManager;

        [Inject]
        IInjectionContainer container;

        public async UniTask Run()
        {
            Debug.Log("BussinessInitializeLoginSceneUI Run");

            //创建开始按钮
            var go = await uiManager.OpenUIAsync("StartButton", IUIManager.Root.BottomRoot);
            container.Bind<StartButton>().ToGameObject(go).AsObjectName(); //attacth一个组件上去
            var component = go.GetComponent<StartButton>();
            Debug.Assert(component != null, "component is null");

            component.onClicked += Component_onClicked;

            //go = await uiManager.OpenUIAsync("StartButton", IUIManager.Root.BottomRoot);
            //go.transform.position = Vector3.zero;
   
        }


        private void Component_onClicked()
        {
            var dispatcher = container.GetCommandDispatcher();
            dispatcher.Dispatch<SwitchSceneCommand>("Game", "SMFadeTransition");
        }
    }

}
