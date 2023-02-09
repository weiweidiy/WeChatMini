using Adic;
using Cysharp.Threading.Tasks;

namespace HiplayGame
{
    public class DefaultTransitionProvider : ITransitionProvider
    {
        [Inject]
        IAssetLoader assetLoader;

        public async UniTask<ITransition> InstantiateAsync(string transitionName)
        {
            // to do : 增加一个location manager用于定位资源位置
            var go = await assetLoader.InstantiateAsync(transitionName);

            var result = go.GetComponent<ITransition>();

            return result;
        }
    }
}
