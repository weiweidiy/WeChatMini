using Adic;
using Adic.Container;
using UnityEngine;

namespace HiplayGame
{
    public class TransformBindings : IBindingsSetup
    {
        public void SetupBindings(IInjectionContainer container)
        {
            //var go = GameObject.FindGameObjectWithTag("BottomUILayer");
            //container.Bind<Transform>().ToGameObjectWithTag("BottomUILayer").AsObjectName();
        }
    }

}


