using System;
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

        public virtual string Name => GetType().ToString();

        public virtual string Location => GetType().ToString();

        /// <summary>
        /// 底层canvas挂载根节点
        /// </summary>
        public Transform BottomRoot { get; private set; }

        /// <summary>
        /// 中层canvas挂载根节点
        /// </summary>
        public Transform MiddleRoot { get; private set; }

        /// <summary>
        /// 顶层canvas挂载根节点
        /// </summary>
        public Transform TopRoot { get; private set; }


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
            canvas.worldCamera = GameObject.FindWithTag("MainCamera")?.GetComponent<Camera>();
            var cScaler = go.AddComponent<CanvasScaler>();
            go.AddComponent<GraphicRaycaster>();
            cScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            cScaler.referenceResolution = new Vector2(1080, 1920);
            cScaler.matchWidthOrHeight = 0f;
            return go.transform;
        }


        public async virtual UniTask OnEnter() {
            //初始化 BottomRoot
            var bRoot = GameObject.Find(IUIManager.Root.BottomRoot.ToString());
            if (bRoot == null)
            {
                BottomRoot = CreateRoot(IUIManager.Root.BottomRoot.ToString());
            }
            else
            {
                BottomRoot = bRoot.transform;
            }


            var canvas = BottomRoot.GetComponent<Canvas>();
            if (canvas == null)
                throw new Exception("场景中缺少 BottomRoot 的Canvas组件");

            //初始化 MiddleRoot
            var mRoot = GameObject.Find(IUIManager.Root.MiddleRoot.ToString());
            if (mRoot == null)
            {
                MiddleRoot = CreateRoot(IUIManager.Root.MiddleRoot.ToString());
            }
            else
            {
                MiddleRoot = mRoot.transform;
            }


            canvas = MiddleRoot.GetComponent<Canvas>();
            if (canvas == null)
                throw new Exception("场景中缺少 MiddleRoot 的Canvas组件");


            //初始化 TopRoot
            var tRoot = GameObject.Find(IUIManager.Root.TopRoot.ToString());
            if (tRoot == null)
            {
                TopRoot = CreateRoot(IUIManager.Root.TopRoot.ToString());
            }
            else
            {
                TopRoot = tRoot.transform;
            }


            canvas = TopRoot.GetComponent<Canvas>();
            if (canvas == null)
                throw new Exception("场景中缺少 TopRoot 的Canvas组件");

            await UniTask.DelayFrame(0); }

        public virtual void OnEnterTransitionComplete() { }

        public virtual void OnExit() { }

        public virtual void OnExitTransitionStart() { }
    }
}
