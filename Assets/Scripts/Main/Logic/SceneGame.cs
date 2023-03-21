using System;
using Adic;
using Adic.Container;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace HiplayGame
{
    public class SceneGame : BaseScene
    {
        
        [Inject]
        MapsManager mapManager;

        public override string Location => "Game";

        public override void Initialize()
        {
            _container.Bind<BussinessInitializeGameSceneUI>().ToSelf();
        }

        public async override UniTask OnEnter()
        {
            await base.OnEnter();
            //根据数据创建场景
            var dataList = mapManager.CreateMaps();
            Debug.Log(" mapManager " + mapManager.GetHashCode());
            var go = new GameObject("DataGridDebugger");
            _container.Bind<DataGridDebugger>().ToGameObject(go).AsObjectName();


            var logic = _container.Resolve<BussinessInitializeGameSceneUI>();
            await logic.Run();

 

        }

    }

}
