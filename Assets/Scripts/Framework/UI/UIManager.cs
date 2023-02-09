/****************************************************
    文件：UIManager.cs
    作者：嵇春苇
    邮箱: 49595272@qq.com
    日期：2022/12/22 22:20:23
    功能：Nothing
*****************************************************/

using System;
using Adic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace HiplayGame
{
    /// <summary>
    /// 打开/关闭
    /// 窗口是否可以打开多个
    /// 打开动画
    /// 是否模态（带不同类型的mask）
    /// </summary>
    public class UIManager : IUIManager
    {
        /// <summary>
        /// 预制根节点枚举值
        /// </summary>
        public enum Root
        {
            BottomRoot,
            MiddleRoot,
            TopRoot
        }

        /// <summary>
        /// 资源加载器
        /// </summary>
        [Inject]
        IAssetLoader assetLoader;

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

        /// <summary>
        /// 初始化管理器
        /// </summary>
        public void Initialize()
        {
            //初始化 BottomRoot
            var bRoot = GameObject.Find(Root.BottomRoot.ToString());
            if(bRoot == null)
                BottomRoot = CreateRoot(Root.BottomRoot.ToString());
            var canvas = bRoot.GetComponent<Canvas>();
            if (canvas == null)
                throw new Exception("场景中缺少 BottomRoot 的Canvas组件");

            //初始化 MiddleRoot
            var mRoot = GameObject.Find(Root.MiddleRoot.ToString());
            if (mRoot == null)
                MiddleRoot = CreateRoot(Root.MiddleRoot.ToString());
            canvas = mRoot.GetComponent<Canvas>();
            if (canvas == null)
                throw new Exception("场景中缺少 MiddleRoot 的Canvas组件");


            //初始化 TopRoot
            var tRoot = GameObject.Find(Root.TopRoot.ToString());
            if (tRoot == null)
                TopRoot = CreateRoot(Root.TopRoot.ToString());
            canvas = tRoot.GetComponent<Canvas>();
            if (canvas == null)
                throw new Exception("场景中缺少 TopRoot 的Canvas组件");

        }

        #region public

        /// <summary>
        /// 打开一个UI
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public GameObject OpenUI(string name, Transform parent)
        {
            return assetLoader.Instantiate(name, parent);
        }

        /// <summary>
        /// 打开一个UI 并挂载到预制的根节点上
        /// </summary>
        /// <param name="name"></param>
        /// <param name="root"></param>
        /// <returns></returns>
        public GameObject OpenUI(string name, Root root)
        {
            var tranRoot = GetRootTransform(root);
            return OpenUI(name, tranRoot);
        }

        /// <summary>
        /// 打开一个UI（异步）
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public UniTask<GameObject> OpenUIAsync(string name, Transform parent)
        {
            Debug.Log("OpenUI " + name);
            Debug.Assert(assetLoader != null, "resourcesManager is null");

            return assetLoader.InstantiateAsync(name, parent);
        }

        /// <summary>
        /// 异步打开一个UI 并挂载到预制的根节点上
        /// </summary>
        /// <param name="name"></param>
        /// <param name="root"></param>
        /// <returns></returns>
        public UniTask<GameObject> OpenUIAsync(string name, Root root)
        {
            var tranRoot = GetRootTransform(root);
            return OpenUIAsync(name, tranRoot);
        }

        #endregion

        #region private
        /// <summary>
        /// 创建一个canvas根节点
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Transform CreateRoot(string name)
        {
            var go = new GameObject(name);
            go.AddComponent<RectTransform>();
            go.AddComponent<Canvas>();
            var cScaler = go.AddComponent<CanvasScaler>();
            go.AddComponent<GraphicRaycaster>();
            cScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            cScaler.referenceResolution = new Vector2(1080, 1920);
            cScaler.matchWidthOrHeight = 0f;
            return go.transform;
        }

        /// <summary>
        /// 获取一个指定的根节点transform
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        Transform GetRootTransform(Root root)
        {
            switch(root)
            {
                case Root.BottomRoot:
                    return BottomRoot;
                case Root.MiddleRoot:
                    return MiddleRoot;
                case Root.TopRoot:
                    return TopRoot;
                default:
                    return BottomRoot;
            }
        }
        #endregion
    }
}