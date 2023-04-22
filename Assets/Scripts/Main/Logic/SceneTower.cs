using Cysharp.Threading.Tasks;
using UnityEngine;

namespace HiplayGame
{
    public class SceneTower : BaseScene
    {
        public override string Location => "Level5";

        public override void Initialize()
        {
            _container.Bind<TowerManager>().ToSingleton();
            _container.Bind<TeamManager>().ToSingleton();
            _container.Bind<UserManager>().ToSingleton();
            _container.Bind<CommandManager>().ToSingleton();


            _container.Bind<DouyinMessageHandle>().ToSingleton();

            var teamManager = _container.Resolve<TeamManager>();
            var userManager = _container.Resolve<UserManager>();
            var towerManager = _container.Resolve<TowerManager>();

            teamManager.onMemberAdded += TeamManager_onMemberAdded;

        }

        private void TeamManager_onMemberAdded(UserObject obj, Team team)
        {
            Debug.Log(obj.nickName + " º”»Î " + team.ToString());
        }

        public async override UniTask OnEnter()
        {



            var reciever = _container.Resolve<IMessageReciever>();
            reciever.onMessageRecieved += OnDouyinMessageRecieved;
            reciever.Connect();


            await base.OnEnter();
            //var logic = _container.Resolve<BusinessInitializeLoginSceneUI>();
            //await logic.Run();
        }

        private void OnDouyinMessageRecieved(string message)
        {
            var handle = _container.Resolve<DouyinMessageHandle>();
            handle.OnMessage(message);
        }
    }
}
