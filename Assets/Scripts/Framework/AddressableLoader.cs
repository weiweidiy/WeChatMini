using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace hiplaygame
{

    public class AddressableLoader : IAssetLoader
    {
        /// <summary>
        /// 缓存加载的资源
        /// </summary>
        Dictionary<string, AsyncOperationHandle> assetCache = new Dictionary<string, AsyncOperationHandle>();


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

        public SceneInstance LoadScene(string sceneName)
        {
            //不支持
            var result = Addressables.LoadSceneAsync(sceneName).WaitForCompletion();
            return result;
        }



        public async UniTask<GameObject> InstantiateAsync(string resourceName, Transform parent = null, bool instantiateInWorldSpace = false)
        {
            var handle = Addressables.InstantiateAsync(resourceName, parent, instantiateInWorldSpace);
            await handle.ToUniTask();
            if (handle.IsDone && handle.Result != null)
            {
                return handle.Result;
            }

            throw new Exception($"实例化GameObject{resourceName}异常！");
        }


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

        public void Release(string resourceName)
        {
            if (assetCache.ContainsKey(resourceName))
            {
                Addressables.Release(assetCache[resourceName]);
            }
        }
    }

}
