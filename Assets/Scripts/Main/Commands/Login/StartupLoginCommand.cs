using Adic;
using Adic.Container;
using System;
using UnityEngine;


namespace HiplayGame
{
    [Obsolete("直接在 LoginScene 中写逻辑代码")]
    public class StartupLoginCommand : Command
    {
        [Inject]
        IInjectionContainer container;

        [Inject]
        IUIManager uiManager;

        [Inject]
        LocalDataManager dataManager;

        public override void Execute(params object[] parameters)
        {
            Debug.Log("StartupMenuCommand " + dataManager.GetHashCode());

        }

    }
}


