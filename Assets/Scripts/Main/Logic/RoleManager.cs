using Adic;
using Adic.Container;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace HiplayGame
{
    public class RoleManager
    {
        [Inject]
        IInjectionContainer _container;

        [Inject]
        public void Initialize()
        {
            _container.Bind<Role>().ToSelf();
        }

        public void CreateRole(string name, Team team, Action<Role> complete)
        {
            var role = _container.Resolve<Role>();
            role.CreateAsync(name, _container.Resolve<GroundView>().GetRoleSpawnTransform(team), (go) =>
             {
                 role.SetLocalPosition(new Vector3(0, 0, Random.Range(-3f, 3f)));
                 complete?.Invoke(role);
             });
        }
    }
}
