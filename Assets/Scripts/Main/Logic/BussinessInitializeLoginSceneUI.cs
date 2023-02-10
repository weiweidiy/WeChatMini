using Adic;
using Adic.Container;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace HiplayGame
{
    public class BussinessInitializeLoginSceneUI : BaseBussiness
    {
        [Inject]
        IUIManager uiManager;

        public override async UniTask Run()
        {
            Debug.Log("BussinessInitializeLoginSceneUI Run");

            //创建开始按钮
            var go = await uiManager.OpenUIAsync("StartButton", IUIManager.Root.BottomRoot);
            _container.Bind<CommonButton>().ToGameObject(go).AsObjectName(); //attacth一个组件上去
            var btnStart = go.GetComponent<CommonButton>();
            Debug.Assert(btnStart != null, "component is null");

            btnStart.onClicked += Component_onClicked;

            //go = await uiManager.OpenUIAsync("StartButton", IUIManager.Root.BottomRoot);
            //go.transform.position = Vector3.zero;
        }

        private void Component_onClicked()
        {
            var dispatcher = _container.GetCommandDispatcher();
            dispatcher.Dispatch<SwitchSceneCommand>("Game", "SMFadeTransition");
        }
    }

}
