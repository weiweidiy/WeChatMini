using Adic;
using Adic.Container;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace HiplayGame
{
    public abstract class BaseScene : IScene
    {

        [Inject]
        protected IInjectionContainer _container;

        [Inject]
        protected IUIManager _uiManager;

        public virtual string Name => GetType().ToString();

        public virtual string Location => GetType().ToString();

        [Inject]
        public virtual void Initialize() { }


        /// <summary>
        /// 创建一个canvas根节点
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Transform CreateRoot(string name)
        {
            var go = new GameObject(name);
            go.AddComponent<RectTransform>();
            var canvas = go.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = GameObject.FindWithTag("UICamera")?.GetComponent<Camera>();
            int layerMask = 0 << LayerMask.NameToLayer("UI");
            go.layer = 5;
            var cScaler = go.AddComponent<CanvasScaler>();
            go.AddComponent<GraphicRaycaster>();
            cScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            cScaler.referenceResolution = new Vector2(1080, 1920);
            cScaler.matchWidthOrHeight = 0f;
            return go.transform;
        }


        public async virtual UniTask OnEnter()
        {
            //初始化 BottomRoot
            _uiManager.BottomRoot = CreateRoot(IUIManager.Root.BottomRoot.ToString());


            _uiManager.MiddleRoot = CreateRoot(IUIManager.Root.MiddleRoot.ToString());

            _uiManager.TopRoot = CreateRoot(IUIManager.Root.TopRoot.ToString());

            await UniTask.DelayFrame(0);
        }

        public virtual void OnEnterTransitionComplete() { }

        public virtual void OnExit() { }

        public virtual void OnExitTransitionStart() { }
    }
}
