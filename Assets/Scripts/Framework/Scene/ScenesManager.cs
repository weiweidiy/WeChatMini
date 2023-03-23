using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adic;
using Cysharp.Threading.Tasks;
using UnityEngine;
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

        [Inject]
        ISceneProvider sceneProvider; // bug 无法跨场景

        IScene CurScene;

        /// <summary>
        /// 切换一个场景
        /// </summary>
        /// <param name="targetScene"></param>
        /// <returns></returns>
        public async UniTask<SceneInstance> SwitchSceneAsync(string target)
        {
            var targetScene = sceneProvider.GetScene(target);

            if (targetScene == null)
                throw new Exception("目标场景为空，不能切换场景 " + targetScene.Name);

            if (CurScene != null)
                CurScene.OnExit();

            var sceneObj = await assetLoader.LoadSceneAsync(targetScene.Location);

            //sceneProvider = GameObject.Find("Context").GetComponent<ContextRoot>().containers[0].Resolve<ISceneProvider>();

            CurScene = sceneProvider.GetScene(target);

            await CurScene.OnEnter();

            return sceneObj;
        }

        /// <summary>
        /// 切换一个场景，带过渡效果
        /// </summary>
        /// <param name="targetScene"></param>
        public async UniTask<SceneInstance> SwitchSceneAsync(string target, ITransition transition)
        {
            if (transition == null)
                return await SwitchSceneAsync(target);

            var state = await transition.TransitionOut();

            var sceneObj = await SwitchSceneAsync(target);

            await transition.TransitionIn();

            CurScene.OnEnterTransitionComplete();

            return sceneObj;
        }


    }
}
