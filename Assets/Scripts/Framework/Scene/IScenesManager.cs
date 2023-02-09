using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace HiplayGame
{
    /// <summary>
    /// 负责提供场景数据
    /// </summary>
    public interface IScenesManager
    {

        /// <summary>
        /// 切换到一个场景
        /// </summary>
        /// <param name="scene"></param>
        UniTask<SceneInstance> SwitchSceneAsync(string scene , ITransition transition);

        UniTask<SceneInstance> SwitchSceneAsync(string scene);
    }
}
