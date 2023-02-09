using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders;


namespace HiplayGame
{

    /// <summary>
    /// * 资源管理器
    /// *
    /// * 负责 加载/缓存/释放 游戏一切用到的资源
    /// *
    /// *
    /// </summary>
    public interface IAssetLoader
    {
        /// <summary>
        /// 加载一个资源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        UniTask<T> LoadAssetAsync<T>(string resourceName) where T : class;

        /// <summary>
        /// 加载场景： to do: 可以单独拆分场景管理
        /// </summary>
        /// <param name="sceneName"></param>
        /// <returns></returns>
        UniTask<SceneInstance> LoadSceneAsync(string sceneName);

        /// <summary>
        /// 同步加载场景
        /// </summary>
        /// <param name="secenName"></param>
        /// <returns></returns>
        SceneInstance LoadScene(string secenName);

        /// <summary>
        /// 实例化游戏对象： to do: 可以单独拆分成GameObjectManager
        /// </summary>
        /// <param name="resourceName"></param>
        /// <param name="parent"></param>
        /// <param name="instantiateInWorldSpace"></param>
        /// <returns></returns>
        UniTask<GameObject> InstantiateAsync(string resourceName, Transform parent = null, bool instantiateInWorldSpace = false);

        /// <summary>
        /// 同步实例化游戏对象
        /// </summary>
        /// <param name="resourceName"></param>
        /// <param name="parent"></param>
        /// <param name="instantiateInWorldSpace"></param>
        /// <returns></returns>
        GameObject Instantiate(string resourceName, Transform parent = null, bool instantiateInWorldSpace = false);

        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="resourceName"></param>
        void Release(string resourceName);
    }

}
