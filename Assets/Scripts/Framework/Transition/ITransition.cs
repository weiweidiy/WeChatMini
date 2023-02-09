using Cysharp.Threading.Tasks;

namespace HiplayGame
{
    public interface ITransition
    {
        UniTask<SMTransitionState> TransitionOut();
        UniTask<SMTransitionState> TransitionIn();
    }
}
