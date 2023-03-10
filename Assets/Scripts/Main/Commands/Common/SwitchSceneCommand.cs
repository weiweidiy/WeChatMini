using Adic;
using System;

namespace HiplayGame
{
    public class SwitchSceneCommand : Command
    {
        //[Inject]
        //IAssetLoader resourcesManager;

        [Inject]
        IScenesManager sceneManager;

        [Inject]
        ITransitionProvider transitionProvider;

        [Inject]
        ISceneProvider sceneProvider;

        /// <summary>
        /// 0:场景名 [1]：转场动画名
        /// </summary>
        /// <param name="parameters"></param>
        public override async void Execute(params object[] parameters)
        {
            

            if (parameters.Length == 0)
                throw new ArgumentNullException("SwitchSceneCommand 参数不能为null!，0：场景名 1：是否过度动画");

            //如果是持续的，需要保持
            this.Retain();

            var transitionName = parameters.Length > 1 ? parameters[1].ToString() : "SMFadeTransition";
            var targetSceneName = parameters[0].ToString();
            //var scene = sceneProvider.GetScene(targetSceneName);
            var transition = await transitionProvider.InstantiateAsync(transitionName);
            var sceneInstance = await sceneManager.SwitchSceneAsync(targetSceneName, transition);

            //如果是持续的，需要释放
            this.Release();
        }
    }
}


