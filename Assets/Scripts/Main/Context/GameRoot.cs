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
            var container = this.AddContainer<InjectionContainer>()
              .RegisterExtension<UnityBindingContainerExtension>()
              //通用模块管理器绑定
              .SetupBindings<CommonClassBindings>()
              //通用命令绑定
              .SetupBindings<CommonCommandsBindings>()
              //游戏逻辑模块绑定
              .SetupBindings<GameClassBindings>()
              //游戏命令绑定
              .SetupBindings<GameCommandsBindings>();

            //获取命令分发器
            dispatcher = container.GetCommandDispatcher();
        }

        public override void Init()
        {
            dispatcher.Dispatch<StartupGameCommand>();
        }
    }
}