/****************************************************
    文件：TestAdic.cs
    作者：嵇春苇
    邮箱: 49595272@qq.com
    日期：2022/12/21 18:16:14
    功能：Nothing
*****************************************************/


using Adic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace hiplaygame.test
{
    public class A
    {
        public int value = 0;
    }

    public class TestAdic : ContextRoot
    {
        ICommandDispatcher _dispatcher;
        public override void Init()
        {
            throw new System.NotImplementedException();

            
        }

        public override void SetupContainers()
        {
            var container = this.AddContainer<InjectionContainer>();
            _dispatcher = container.RegisterExtension<CommanderContainerExtension>()
                    .RegisterCommand<SwitchSceneCommand>()
                    .GetCommandDispatcher();
            container.Bind<A>().ToSingleton();
        }
    }
}