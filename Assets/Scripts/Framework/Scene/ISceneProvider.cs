using Adic;

namespace HiplayGame
{
    public interface ISceneProvider
    {
        /// <summary>
        /// 获取下一个场景
        /// </summary>
        /// <returns></returns>
        IScene GetNextScene(string currentSceneName);

        IScene GetScene(string sceneName);
    }
}
