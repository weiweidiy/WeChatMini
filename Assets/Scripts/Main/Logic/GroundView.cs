using Adic;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace HiplayGame
{
    public class GroundView
    {
        [Inject]
        IAssetLoader assetLoader;

        Transform roleSpawnL = null;
        Transform roleSpawnR = null;
        List<Transform> leftGroup = new List<Transform>();
        List<Transform> rightGroup = new List<Transform>();


        public Transform GetGoundPositionTransform(Team side, int index)
        {
            switch (side)
            {
                case Team.Left:
                    {
                        return leftGroup[index];
                    }
                case Team.Right:
                    {
                        return rightGroup[index];
                    }

            }

            return null;
        }

        public Transform GetRoleSpawnTransform(Team team)
        {
            switch (team)
            {
                case Team.Left:
                    return roleSpawnL;
                case Team.Right:
                    return roleSpawnR;
            }
            return null;
        }


        public async UniTask<GameObject> CreateAsync(string address)
        {
            var go = await assetLoader.InstantiateAsync(address);

            var left = go.transform.Find("PositionL");
            foreach (var pos in left)
            {
                leftGroup.Add(pos as Transform);
            }

            var right = go.transform.Find("PositionR");
            foreach (var pos in right)
            {
                rightGroup.Add(pos as Transform);
            }

            roleSpawnL = go.transform.Find("RoleSpawnL");
            roleSpawnR = go.transform.Find("RoleSpawnR");

            return go;
        }
    }

}
