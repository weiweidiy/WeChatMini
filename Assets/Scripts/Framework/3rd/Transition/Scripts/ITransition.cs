using Cysharp.Threading.Tasks;

namespace hiplaygame
{
    public interface ITransition
    {
        UniTask<SMTransitionState> TransitionOut();
        UniTask<SMTransitionState> TransitionIn();
    }
}
