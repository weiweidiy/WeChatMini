using Adic;
using UnityEngine;

namespace HiplayGame
{
    public class CommandSummonAttacker
    {
        [Inject]
        TeamManager teamManager;

        public void Summon(UserObject user, int attackerType)
        {
            //检查是否是左队
            if (teamManager.Existed(Team.Left, user.userId))
            {
                EnemyManager.instance.CreateEnemy(user, attackerType);
            }
            else
            {
                Debug.LogError("只有攻击方可以召唤");
            }
        }
    }
}
