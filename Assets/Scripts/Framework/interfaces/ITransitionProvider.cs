using Cysharp.Threading.Tasks;

namespace hiplaygame
{
    public interface ITransitionProvider
    {
        UniTask<ITransition>  InstantiateAsync(string transitionType);
    }
}
