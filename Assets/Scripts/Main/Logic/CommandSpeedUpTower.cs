using Adic;
using UnityEngine;

namespace HiplayGame
{
    public class CommandSpeedUpTower
    {
        [Inject]
        TeamManager teamManager;

        [Inject]
        TowerManager towerManager;
        public void Speedup(UserObject user, float k)
        {
            if (!teamManager.Existed(Team.Right, user.userId))
            {
                Debug.LogError("只有防御方可以加速塔");
                return;
            }

            var tower = towerManager.GetTower(user.userId);
            if (tower == null)
            {
                Debug.LogError("您还没有塔");
                return;
            }

            tower.Speedup(k);
        }
    }
}
