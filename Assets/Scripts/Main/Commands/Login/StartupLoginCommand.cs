using Adic;
using Adic.Container;
using UnityEngine;


namespace HiplayGame
{
    public class StartupLoginCommand : Command
    {
        [Inject]
        IInjectionContainer container;

        [Inject]
        IUIManager uiManager;


        public override void Execute(params object[] parameters)
        {
            Debug.Log("StartupMenuCommand");

        }

    }
}


