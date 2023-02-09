using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adic;
using Cysharp.Threading.Tasks;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace HiplayGame
{

    /// <summary>
    /// 负责场景的切换
    /// </summary>
    public class ScenesManager : IScenesManager
    {
        [Inject]
        IAssetLoader assetLoader;

        /// <summary>
        /// 当前场景
        /// </summary>
        public IScene CurScene { get; private set; }

        /// <summary>
        /// 切换一个场景
        /// </summary>
        /// <param name="targetScene"></param>
        /// <returns></returns>
        public async UniTask<SceneInstance> SwitchSceneAsync(IScene targetScene)
        {
            if (targetScene == null)
                throw new Exception("目标场景为空，不能切换场景 " + targetScene.Name);

            if(CurScene != null)
                CurScene.OnExit();

            var sceneObj = await assetLoader.LoadSceneAsync(targetScene.Location);

            CurScene = targetScene;

            CurScene.OnEnter();

            return sceneObj;
        }

        /// <summary>
        /// 切换一个场景，带过渡效果
        /// </summary>
        /// <param name="targetScene"></param>
        public async UniTask<SceneInstance> SwitchSceneAsync(IScene targetScene, ITransition transition)
        {       
            if (transition == null)
                return await SwitchSceneAsync(targetScene);

            var state = await transition.TransitionOut();

            var sceneObj = await SwitchSceneAsync(targetScene);

            await transition.TransitionIn();

            CurScene.OnEnterTransitionComplete();

            return sceneObj;
        }


    }
}
