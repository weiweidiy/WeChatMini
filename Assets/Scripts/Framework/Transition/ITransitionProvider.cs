using Cysharp.Threading.Tasks;

namespace HiplayGame
{
    public interface ITransitionProvider
    {
        UniTask<ITransition>  InstantiateAsync(string transitionType);
    }
}
