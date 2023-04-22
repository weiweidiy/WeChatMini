using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace HiplayGame
{

    public class AddressableLoader : IAssetLoader
    {
        /// <summary>
        /// 缓存加载的资源
        /// </summary>
        Dictionary<string, AsyncOperationHandle> assetCache = new Dictionary<string, AsyncOperationHandle>();

        /// <summary>
        /// 加载一个资源（异步）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        public async UniTask<T> LoadAssetAsync<T>(string resourceName) where T : class
        {
            //如果有缓存，则直接返回
            if (assetCache.ContainsKey(resourceName))
            {
                Debug.Log($"从缓存中获取{resourceName}");
                return assetCache[resourceName].Result as T;
            }
            else
            {
                var handle = Addressables.LoadAssetAsync<T>(resourceName);
                await handle.ToUniTask();

                if (handle.IsDone && handle.Result != null)
                {
                    assetCache.Add(resourceName, handle);
                    return handle.Result;
                }

                throw new Exception($"加载资源{resourceName}异常！");
            }
        }


        /// <summary>
        /// 加载场景(异步)
        /// </summary>
        /// <param name="sceneName"></param>
        /// <returns></returns>
        public async UniTask<SceneInstance> LoadSceneAsync(string sceneName)
        {
            var handle = Addressables.LoadSceneAsync(sceneName);
            await handle.ToUniTask();
            if (handle.IsDone)
            {
                return handle.Result;
            }

            throw new Exception($"加载场景{sceneName}异常！");
        }

        /// <summary>
        /// 加载场景（同步）
        /// </summary>
        /// <param name="sceneName"></param>
        /// <returns></returns>
        public SceneInstance LoadScene(string sceneName)
        {
            //不支持
            var result = Addressables.LoadSceneAsync(sceneName).WaitForCompletion();
            return result;
        }


        /// <summary>
        /// 实例化游戏对象(异步)
        /// </summary>
        /// <param name="resourceName"></param>
        /// <param name="parent"></param>
        /// <param name="instantiateInWorldSpace"></param>
        /// <returns></returns>
        public async UniTask<GameObject> InstantiateAsync(string resourceName, Transform parent = null, bool instantiateInWorldSpace = false, Action<GameObject> complete = null)
        {
            var handle = Addressables.InstantiateAsync(resourceName, parent, instantiateInWorldSpace);
            await handle.ToUniTask();
            if (handle.IsDone && handle.Result != null)
            {
                complete?.Invoke(handle.Result);
                return handle.Result;
            }

            throw new Exception($"实例化GameObject{resourceName}异常！");
        }

        public async UniTask<GameObject> InstantiateAsync(string resourceName, Vector3 position, Quaternion quaternion, Transform parent = null, Action<GameObject> complete = null)
        {
            var handle = Addressables.InstantiateAsync(resourceName, position, quaternion, parent);
            await handle.ToUniTask();
            if (handle.IsDone && handle.Result != null)
            {
                complete?.Invoke(handle.Result);
                return handle.Result;
            }

            throw new Exception($"实例化GameObject{resourceName}异常！");
        }

        /// <summary>
        /// 实例化游戏对象（同步）
        /// </summary>
        /// <param name="resourceName"></param>
        /// <param name="parent"></param>
        /// <param name="instantiateInWorldSpace"></param>
        /// <returns></returns>
        public GameObject Instantiate(string resourceName, Transform parent = null, bool instantiateInWorldSpace = false)
        {
            GameObject result = null;
            try
            {
                result = Addressables.InstantiateAsync(resourceName, parent, instantiateInWorldSpace).WaitForCompletion();
            }
            catch (Exception e)
            {
                Debug.LogError("同步实例化错误 " + e.Message);
            }

            Debug.Assert(result != null, "result = null");
            return result;
        }

        /// <summary>
        /// 是否资源
        /// </summary>
        /// <param name="resourceName"></param>
        public void Release(string resourceName)
        {
            if (assetCache.ContainsKey(resourceName))
            {
                Addressables.Release(assetCache[resourceName]);
            }
        }
    }

}
