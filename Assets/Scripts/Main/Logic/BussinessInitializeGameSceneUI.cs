using Adic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace HiplayGame
{
    public class BussinessInitializeGameSceneUI : IGameBussiness
    {
        [Inject]
        IUIManager uiManager;

        public async UniTask Run()
        {
            Debug.Log("BussinessInitializeGameSceneUI Run");

            await UniTask.Delay(0);
        }
    }

}
