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
        IAssetLoader resourcesManager;

        public GameObject OpenUI(string name, Transform parent)
        {
            return resourcesManager.Instantiate(name, parent);
        }

        public UniTask<GameObject> OpenUIAsync(string name, Transform parent)
        {
            Debug.Log("OpenUI");
            Debug.Assert(resourcesManager != null, "resourcesManager is null");

            return resourcesManager.InstantiateAsync(name, parent);
        }
    }
}