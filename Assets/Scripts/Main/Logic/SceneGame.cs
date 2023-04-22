using Cysharp.Threading.Tasks;

namespace HiplayGame
{
    public class SceneGame : BaseScene
    {

        //[Inject]
        //MapsManager mapManager;

        public override string Location => "Game";

        public override void Initialize()
        {
            _container.Bind<BusinessInitializeGame>().ToSingleton();
            _container.Bind<BusinessInitializeGameSceneView>().ToSingleton();
        }

        public async override UniTask OnEnter()
        {
            await base.OnEnter();
            //根据数据创建场景
            //var dataList = mapManager.CreateMaps();
            //Debug.Log(" mapManager " + mapManager.GetHashCode());
            //var go = new GameObject("DataGridDebugger");
            //_container.Bind<DataGridDebugger>().ToGameObject(go).AsObjectName();

            var logic = _container.Resolve<BusinessInitializeGame>();
            await logic.Run();

            var view = _container.Resolve<BusinessInitializeGameSceneView>();
            await view.Run();



        }

    }

}
