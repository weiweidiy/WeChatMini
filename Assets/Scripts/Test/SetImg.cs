using Adic;
using Cysharp.Threading.Tasks;
using hiplaygame;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
public class SetImg : MonoBehaviour
{
    [Inject]
    IAssetLoader assetLoader;

    async void Start()
    {
        //Addressables.LoadAssetAsync<Sprite>("icon").Completed += SetImg_Completed;

        var sprite = await assetLoader.LoadAssetAsync<Sprite>("icon");

        GetComponent<Image>().sprite = sprite;

        //Test();
    }

    private void SetImg_Completed(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<Sprite> obj)
    {
        GetComponent<Image>().sprite = obj.Result;
    }

    //async void Test()
    //{
    //    await UniTask.Delay(2000);
    //    Debug.Log("UniTask延迟调用");
    //    return;
    //}

    public async void Test()
    {
        var handle = Addressables.DownloadDependenciesAsync("icon"); //下载下来的是一个bundle
        await handle.ToUniTask();
        var r = handle.Result;
    }
}
