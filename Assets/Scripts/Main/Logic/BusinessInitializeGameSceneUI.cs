using Adic;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace HiplayGame
{
    public class GroundView
    {
        public enum Side
        {
            Left,
            Right
        }

        [Inject]
        IAssetLoader assetLoader;

        List<Transform> leftGroup = new List<Transform>();
        List<Transform> rightGroup = new List<Transform>();


        public Transform GetGoundPositionTransform(Side side, int index)
        {
            switch (side)
            {
                case Side.Left:
                    {
                        return leftGroup[index];
                    }
                case Side.Right:
                    break;
            }

            return null;
        }

        public async UniTask<GameObject> CreateAsync(string address)
        {
            var go = await assetLoader.InstantiateAsync(address);

            var left = go.transform.Find("PositionL");
            foreach (var pos in left)
            {
                leftGroup.Add(pos as Transform);
            }

            return go;
        }
    }
    public class BusinessInitializeGameSceneUI : BaseBusiness
    {
        [Inject]
        IUIManager uiManager;

        [Inject]
        IAssetLoader assetLoader;

        protected override void Initialize()
        {
            base.Initialize();
            Debug.Log("BussinessInitializeGameSceneUI Initialize");

            _container.Bind<GroundView>().ToSingleton();
        }

        public async override UniTask Run()
        {
            Debug.Log("BussinessInitializeGameSceneUI Run");

            //创建地图
            var groundView = _container.Resolve<GroundView>();
            var go = await groundView.CreateAsync("Ground");

            for (int i = 0; i < 4; i++)
            {
                //创建角色
                var role = assetLoader.InstantiateAsync("PolyArtWizardStandardMat", groundView.GetGoundPositionTransform(GroundView.Side.Left, i));
            }



            await UniTask.Delay(0);
        }


    }

}
