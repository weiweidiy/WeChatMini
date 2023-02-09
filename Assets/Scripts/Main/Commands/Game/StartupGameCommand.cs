using Adic;
using Adic.Container;
using UnityEngine;

namespace HiplayGame
{
    public class StartupGameCommand : Command
    {
        [Inject]
        IInjectionContainer container;

        [Inject]
        MapsManager mapManager;
        public override void Execute(params object[] parameters)
        {
            //assetLoader.InstantiateAsync("GameController");

            Debug.Log("StartupGameCommand Execute");

            //根据数据创建场景
            var dataList = mapManager.CreateMaps();
            var go = new GameObject("DataGridDebugger");
            container.Bind<DataGridDebugger>().ToGameObject(go).AsObjectName();

            //根据数据创建UI

            //根据数数据创建脚本
        }
    }
}


