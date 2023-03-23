using Adic;

namespace HiplayGame
{

    public class LoginRoot : ContextRoot
    {
        protected ICommandDispatcher dispatcher;

        public override void SetupContainers()
        {

            var container = this.AddContainer/*<InjectionContainer>*/(new InjectionContainer(StaticReflectionCache.cache))
                .RegisterExtension<UnityBindingContainerExtension>()
               //通用模块管理器绑定
               .SetupBindings<CommonClassBindings>()
               //通用命令绑定
               //.SetupBindings<CommonCommandsBindings>()
               //登录命令模块绑定
               .SetupBindings<LoginCommandsBindings>()
               //绑定游戏对象
               .SetupBindings<TransformBindings>();


            //获取命令分发器
            dispatcher = container.GetCommandDispatcher();


        }

        public override void Init()
        {
            //dispatcher.Dispatch<StartupLoginCommand>();
        }
    }
}


