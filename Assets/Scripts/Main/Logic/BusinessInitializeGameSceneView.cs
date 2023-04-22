using Adic;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace HiplayGame
{

    public enum Team
    {
        None,
        Left,
        Right
    }

    public class BusinessInitializeGameSceneView : BaseBusiness
    {
        [Inject]
        IUIManager uiManager;

        [Inject]
        IAssetLoader assetLoader;

        [Inject]
        HeroManager heroManager;

        [Inject]
        GameManager gameManager;

        protected override void Initialize()
        {
            base.Initialize();
            Debug.Log("BussinessInitializeGameSceneUI Initialize");

            _container.Bind<GroundView>().ToSingleton();
        }

        public async override UniTask Run()
        {
            Debug.Log("BussinessInitializeGameSceneUI Run");

            //初始化场景
            var groundView = _container.Resolve<GroundView>();
            var gournd = await groundView.CreateAsync("Ground");

            //测试按钮
            var go = await uiManager.OpenUIAsync("StartButton", IUIManager.Root.BottomRoot);
            _container.Bind<CommonButton>().ToGameObject(go).AsObjectName();
            var btnStart = go.GetComponent<CommonButton>();
            btnStart.onClicked += Component_onClicked;


            var go2 = await uiManager.OpenUIAsync("StartButton", IUIManager.Root.BottomRoot);
            _container.Bind<CommonButton>().ToGameObject(go2).AsObjectName();
            go2.transform.localPosition += new Vector3(200f, 0, 0);
            var btnStart2 = go2.GetComponent<CommonButton>();
            btnStart2.onClicked += Component2_onClicked;

            ////创建英雄
            for (int i = 0; i < 5; i++)
            {
                heroManager.CreateHero(0, groundView.GetGoundPositionTransform(Team.Left, i));
                heroManager.CreateHero(0, groundView.GetGoundPositionTransform(Team.Right, i));
            }

            //初始化ui


            //状态嵇切换
            gameManager.StartRegist();

            await UniTask.Delay(0);
        }

        private void Component2_onClicked()
        {
            //throw new NotImplementedException();
            _container.Resolve<GameManager>().StartCountDown();
        }

        private void Component_onClicked()
        {
            Debug.Log("Component_onClicked");
            var reciever = _container.Resolve<IMessageReciever>();
            var msg = new UserObject();
            msg.method = "WebcastChatMessage";
            msg.nickName = "weiwei";
            msg.content = "1";
            string json = JsonConvert.SerializeObject(msg);
            reciever.Notify(json);
        }

    }

}
