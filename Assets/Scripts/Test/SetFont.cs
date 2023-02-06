using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class SetFont : MonoBehaviour
{
    private void Awake()
    {
        //Addressables.LoadAssetAsync<UnityEngine.Font>("font").WaitForCompletion();

    }
    void Start()
    {
        Addressables.LoadAssetAsync<UnityEngine.Font>("font").Completed += SetFont_Completed;


    }

    private void SetFont_Completed(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<Font> obj)
    {
        GetComponent<Text>().font = obj.Result;
    }


}
