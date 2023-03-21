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
               //ͨ��ģ���������
               .SetupBindings<CommonClassBindings>()
               //ͨ�������
               //.SetupBindings<CommonCommandsBindings>()
               //��¼����ģ���
               .SetupBindings<LoginCommandsBindings>()
               //����Ϸ����
               .SetupBindings<TransformBindings>();


            //��ȡ����ַ���
            dispatcher = container.GetCommandDispatcher();


        }

        public override void Init()
        {
            //dispatcher.Dispatch<StartupLoginCommand>();
        }
    }
}


