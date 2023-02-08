/****************************************************
    文件：UIManager.cs
    作者：嵇春苇
    邮箱: 49595272@qq.com
    日期：2022/12/22 22:20:23
    功能：Nothing
*****************************************************/

using Adic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace hiplaygame
{
    /// <summary>
    /// 打开/关闭
    /// 窗口是否可以打开多个
    /// 打开动画
    /// 是否模态
    /// </summary>
    public class UIManager : IUIManager
    {
        [Inject]
        IAssetLoader assetLoader;

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
    }
}