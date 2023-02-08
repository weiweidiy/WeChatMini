namespace hiplaygame
{
    public class DefaultSceneProvider : ISceneProvider
    {
        public IScene GetNextScene(string currentSceneName)
        {
            switch(currentSceneName)
            {
                case "Launcher":
                    return new SceneLogin();
                case "Login":
                    return new SceneGame();
                default:
                    return null;
            }
        }

        public IScene GetScene(string sceneName)
        {
            switch (sceneName)
            {
                case "Login":
                    return new SceneLogin();
                case "Game":
                    return new SceneGame();
                default:
                    return null;
            }
        }
    }
}
