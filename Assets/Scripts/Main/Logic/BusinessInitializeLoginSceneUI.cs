using Adic;
using Cysharp.Threading.Tasks;
using DouyinGame;
using UnityEngine;
using UnityEngine.UI;

namespace HiplayGame
{
    public class BusinessInitializeLoginSceneUI : BaseBusiness
    {
        [Inject]
        IUIManager uiManager;

        protected override void Initialize()
        {
            base.Initialize();

            _container.Bind<LocalData>().ToSingleton();
            _container.Bind<IMessageReciever>().ToSingleton<GameNetwork>();
        }


        public override async UniTask Run()
        {
            Debug.Log("BussinessInitializeLoginSceneUI Run");

            //������ʼ��ť
            var go = await uiManager.OpenUIAsync("StartButton", IUIManager.Root.BottomRoot);
            _container.Bind<CommonButton>().ToGameObject(go).AsObjectName(); //attacthһ�������ȥ
            var btnStart = go.GetComponent<CommonButton>();
            Debug.Assert(btnStart != null, "component is null");

            btnStart.onClicked += Component_onClicked;

            //go = await uiManager.OpenUIAsync("StartButton", IUIManager.Root.BottomRoot);
            //go.transform.position = Vector3.zero;
        }



        private void Component_onClicked()
        {
            var field = GameObject.Find("Canvas/InputField").transform.GetComponent<InputField>();

            _container.Resolve<LocalData>().roomId = field.text;
            var dispatcher = _container.GetCommandDispatcher();
            dispatcher.Dispatch<SwitchSceneCommand>("Game", "SMFadeTransition");
        }
    }

}
