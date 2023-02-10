/****************************************************
    文件：GameRoot.cs
    作者：嵇春苇
    邮箱: 49595272@qq.com
    日期：2022/12/23 17:26:45
    功能：Nothing
*****************************************************/


using Adic;

namespace HiplayGame
{
    public class GameRoot : ContextRoot
    {
        protected ICommandDispatcher dispatcher;

        public override void SetupContainers()
        {
            var container = this.AddContainer<InjectionContainer>(/*new InjectionContainer(StaticReflectionCache.cache)*/)
              .RegisterExtension<UnityBindingContainerExtension>()

              //游戏逻辑模块绑定  游戏类要优先通用类绑定，比如 mapmanager 需要先绑定，然后scene才能注入，否则会反射一个新对象
              .SetupBindings<GameClassBindings>()   
              //游戏命令绑定
              .SetupBindings<GameCommandsBindings>()

              //通用模块管理器绑定
              .SetupBindings<CommonClassBindings>()
              //通用命令绑定
              .SetupBindings<CommonCommandsBindings>();

            //获取命令分发器
            dispatcher = container.GetCommandDispatcher();
        }

        public override void Init()
        {
            dispatcher.Dispatch<StartupGameCommand>();
        }
    }
}