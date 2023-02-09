namespace HiplayGame
{
    public abstract class BaseScene : IScene
    {
        public virtual string Name => GetType().ToString();

        public virtual string Location => GetType().ToString();

        public virtual void OnEnter() 
        {
            //≥ı ºªØUI
        }

        public virtual void OnEnterTransitionComplete() { }

        public virtual void OnExit() { }

        public virtual void OnExitTransitionStart() { }
    }
}
