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

            //�������ݴ�������
            var dataList = mapManager.CreateMaps();
            var go = new GameObject("DataGridDebugger");
            container.Bind<DataGridDebugger>().ToGameObject(go).AsObjectName();

            //�������ݴ���UI

            //���������ݴ����ű�
        }
    }
}


