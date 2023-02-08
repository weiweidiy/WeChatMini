using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adic;
using Cysharp.Threading.Tasks;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace hiplaygame
{

    /// <summary>
    /// 负责场景的切换
    /// </summary>
    public class ScenesManager : IScenesManager
    {
        [Inject]
        IAssetLoader assetLoader;


        public void Initialize()
        {
            
        }

        /// <summary>
        /// 切换一个场景
        /// </summary>
        /// <param name="scene"></param>
        public async UniTask<SceneInstance> SwitchSceneAsync(IScene scene, ITransition transition)
        {       
            var targetSceneLocation = scene.Location;

            if (transition == null)
            {
                var sceneObj = await assetLoader.LoadSceneAsync(targetSceneLocation);

                return sceneObj;
            }
            else
            {
                var state = await transition.TransitionOut();

                var sceneObj = await assetLoader.LoadSceneAsync(targetSceneLocation);

                await transition.TransitionIn();

                return sceneObj;
            }
        }
    }
}
