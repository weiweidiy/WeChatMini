using Adic;

namespace hiplaygame
{
    public class StartupGameCommand : Command
    {
        [Inject]
        IAssetLoader assetLoader;
        public override void Execute(params object[] parameters)
        {
            assetLoader.InstantiateAsync("GameController");
        }
    }
}


