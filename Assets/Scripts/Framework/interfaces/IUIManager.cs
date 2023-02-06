/****************************************************
    文件：IUIManager.cs
    作者：嵇春苇
    邮箱: 49595272@qq.com
    日期：2022/12/22 22:20:7
    功能：Nothing
*****************************************************/

using Cysharp.Threading.Tasks;
using UnityEngine;

namespace hiplaygame
{
    public interface IUIManager
    {
        UniTask<GameObject> OpenUIAsync(string name, Transform parent);

        GameObject OpenUI(string name, Transform parent);
    }
}