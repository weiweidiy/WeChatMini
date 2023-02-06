

//using Cysharp.Threading.Tasks;
//using System;
//using UnityEngine.AddressableAssets;

//public interface IResourcesDownloadClient
//{

//    event Action<float> OnProgress;

//    /// <summary>
//    /// 检查版本是否有更新
//    /// </summary>
//    /// <param name="packageName"></param>
//    /// <returns></returns>
//    UniTask<bool> CheckVersion(string packageName);

//    /// <summary>
//    /// 开始下载
//    /// </summary>
//    /// <param name="packageName"></param>
//    void Download(string packageName);

//    /// <summary>
//    /// 检查下载地址有没有需要下载的内容
//    /// </summary>
//    /// <param name="url"></param>
//    /// <returns></returns>
//    UniTask<long> CheckDownloadSize(string url);
//}

//public class AddressablesDownload : IResourcesDownloadClient
//{

//    public async UniTask<bool> CheckVersion(string packageName)
//    {
//        var handle = Addressables.GetDownloadSizeAsync(packageName);
//        await handle.ToUniTask();
//        return handle.Result > 0;
//    }

//    public async void Download(string packageName)
//    {
//        var handle = Addressables.DownloadDependenciesAsync(packageName);
//        handle.
//        await handle.ToUniTask();
//    }


//    public UniTask<long> CheckDownloadSize(string url)
//    {
//        throw new System.NotImplementedException();
//    }




//    //async void Download(string url)
//    //{
//    //    var handle = Addressables.DownloadDependenciesAsync(url);
//    //    await handle.ToUniTask();
//    //    var r = handle.Result;

//    //    return;
//    //}


//}