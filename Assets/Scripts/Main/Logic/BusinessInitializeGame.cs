using Cysharp.Threading.Tasks;
using UnityEngine;

namespace HiplayGame
{
    public class BusinessInitializeGame : BaseBusiness
    {

        public async override UniTask Run()
        {
            Debug.Log("BusinessInitializeGame Run");


            await UniTask.Delay(0);
        }
    }


}
