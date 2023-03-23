/****************************************************
    文件：IUIManager.cs
    作者：嵇春苇
    邮箱: 49595272@qq.com
    日期：2022/12/22 22:20:7
    功能：Nothing
*****************************************************/

using Cysharp.Threading.Tasks;
using UnityEngine;

namespace HiplayGame
{
    public interface IUIManager
    {
        public enum Root
        {
            BottomRoot,
            MiddleRoot,
            TopRoot
        }

        Transform BottomRoot { get; set; }

        /// <summary>
        /// 中层canvas挂载根节点
        /// </summary>
        Transform MiddleRoot { get; set; }

        /// <summary>
        /// 顶层canvas挂载根节点
        /// </summary>
        Transform TopRoot { get; set; }

        UniTask<GameObject> OpenUIAsync(string name, Transform parent);

        GameObject OpenUI(string name, Transform parent);

        GameObject OpenUI(string name, Root root);

        UniTask<GameObject> OpenUIAsync(string name, Root root);
    }
}