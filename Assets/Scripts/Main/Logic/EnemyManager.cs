using Core.Utilities;
using System.Collections.Generic;
using TowerDefense.Agents;
using TowerDefense.Level;
using UnityEngine;

namespace HiplayGame
{
    public class EnemyManager : Singleton<EnemyManager>
    {
        public List<SpawnInstruction> enemyType;

        public void CreateEnemy(UserObject user, int index)
        {
            Vector3 spawnPosition = enemyType[index].startingNode.GetRandomPointInNodeArea();

            var poolable = Poolable.TryGetPoolable<Poolable>(enemyType[index].agentConfiguration.agentPrefab.gameObject);
            if (poolable == null)
            {
                return;
            }
            var agentInstance = poolable.GetComponent<Agent>();
            agentInstance.transform.position = spawnPosition;
            agentInstance.Initialize();
            agentInstance.SetNode(enemyType[index].startingNode);
            agentInstance.transform.rotation = enemyType[index].startingNode.transform.rotation;
            var enemy = agentInstance.GetComponent<Enemy>();
            enemy.SetName(user.nickName);
            enemy.SetIcon(user.iconUrl);
        }

    }
}
