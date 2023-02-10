
using Adic.Container;
using UnityEngine;

namespace HiplayGame
{
    public class DefaultSceneProvider : ISceneProvider
    {
        IInjectionContainer container;

        public DefaultSceneProvider(IInjectionContainer container)
        {
            Debug.Log("DefaultSceneProvider container :" + container.GetHashCode());
            this.container = container;

            this.container.Bind<SceneGame>().ToSingleton();
            this.container.Bind<SceneLogin>().ToSingleton();
        }

        public IScene GetNextScene(string currentSceneName)
        {
            switch(currentSceneName)
            {
                case "Launcher":
                    return container.Resolve<SceneLogin>();
                case "Login":
                    return container.Resolve<SceneGame>();
                default:
                    return null;
            }
        }

        public IScene GetScene(string sceneName)
        {
            switch (sceneName)
            {
                case "Login":
                    return container.Resolve<SceneLogin>();
                case "Game":
                    return container.Resolve<SceneGame>();
                default:
                    return null;
            }
        }


    }
}
