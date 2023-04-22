using Adic;
using Adic.Container;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace HiplayGame
{
    public class HeroManager
    {
        public event Action<Hero> onHeroCreated;
        public event Action<Hero> onHeroDestroyed;
        [Inject]
        IInjectionContainer _container;

        List<Hero> lstHero = new List<Hero>();

        [Inject]
        public void Initialize()
        {
            _container.Bind<Hero>().ToSelf();

        }

        public void CreateHero(int type, Transform parent)
        {
            var hero = _container.Resolve<Hero>();
            lstHero.Add(hero);
            hero.CreateAsync(parent, (go) =>
            {
                onHeroCreated?.Invoke(hero);
            });
        }

        public bool DestroyHero(Hero hero)
        {
            if (lstHero.Remove(hero))
            {
                hero.Destroy();
                onHeroDestroyed?.Invoke(hero);
                return true;
            }
            return false;
        }

    }
}
