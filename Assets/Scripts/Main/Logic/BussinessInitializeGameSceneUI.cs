using Adic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace HiplayGame
{
    public class BussinessInitializeGameSceneUI : BaseBussiness
    {
        [Inject]
        IUIManager uiManager;

        public async override UniTask Run()
        {
            Debug.Log("BussinessInitializeGameSceneUI Run");

            await UniTask.Delay(0);
        }
    }

}
