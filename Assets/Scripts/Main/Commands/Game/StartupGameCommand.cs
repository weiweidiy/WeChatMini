using Adic;
using Adic.Container;
using UnityEngine;

namespace HiplayGame
{
    public class StartupGameCommand : Command
    {
        [Inject]
        LocalDataManager dataManager;

        public override void Execute(params object[] parameters)
        {
            //assetLoader.InstantiateAsync("GameController");

            Debug.Log("StartupGameCommand Execute " + dataManager.GetHashCode());


            //根据数据创建UI

            //根据数数据创建脚本
        }
    }
}


