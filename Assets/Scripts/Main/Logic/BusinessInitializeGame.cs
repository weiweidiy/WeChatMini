using Adic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace HiplayGame
{

    public class BusinessInitializeGame : BaseBusiness
    {
        [Inject]
        IUIManager uiManager;

        protected override void Initialize()
        {
            base.Initialize();
            Debug.Log("BusinessInitializeGame Initialize");
            //_container.Bind<IMessageReciever>().ToSingleton<GameNetwork>();
            _container.Bind<IMessageReciever>().ToSingleton<DebugGameNetwork>();

            _container.Bind<HeroManager>().ToSingleton();
            _container.Bind<RoleManager>().ToSingleton();
            //_container.Bind<TeamManager>().ToSingleton();
            _container.Bind<UserManager>().ToSingleton();
            _container.Bind<GameManager>().ToSingleton();
            _container.Bind<DouyinMessageHandle>().ToSingleton();


            var heroManager = _container.Resolve<HeroManager>();
            var teamManager = _container.Resolve<TeamManager>();
            var userManager = _container.Resolve<UserManager>();
            //teamManager.onMemberAdded += userManager.AddUser;
            //teamManager.onMemberExited += userManager.RemoveUser;
            heroManager.onHeroCreated += HeroManager_onHeroCreated;

        }

        private void HeroManager_onHeroCreated(Hero obj)
        {
            obj.Rotate(new Vector3(0, -180f, 0));
        }

        public async override UniTask Run()
        {
            Debug.Log("BusinessInitializeGame Run");

            var reciever = _container.Resolve<IMessageReciever>();
            reciever.onMessageRecieved += OnDouyinMessageRecieved;

            await UniTask.Delay(0);
        }



        private void OnDouyinMessageRecieved(string message)
        {
            var handle = _container.Resolve<DouyinMessageHandle>();
            handle.OnMessage(message);
        }
    }

}
