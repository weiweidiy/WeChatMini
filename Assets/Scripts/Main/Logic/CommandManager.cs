using Adic;
using Adic.Container;
using UnityEngine;

namespace HiplayGame
{
    public class CommandManager
    {
        [Inject]
        UserManager userManager;
        [Inject]
        TeamManager teamManager;

        [Inject]
        TowerManager towerManager;

        [Inject]
        protected IInjectionContainer _container;

        [Inject]
        public void Initialize()
        {
            _container.Bind<CommandBuildTower>().ToSingleton();
            _container.Bind<CommandSummonAttacker>().ToSingleton();
            _container.Bind<CommandSpeedUpTower>().ToSingleton();
        }


        public void JoinTeam(UserObject user, Team team)
        {
            if (teamManager.Existed(team, user.userId))
                return;

            if (!userManager.Exist(user))
                userManager.AddUser(user);

            teamManager.JoinTeam(team, user);
        }

        public void Summon(UserObject user, int attackerType)
        {
            var team = teamManager.Existed(user.userId);
            if (team == Team.None)
            {
                Debug.LogError("请先加入一个队伍");
                return;
            }

            var command = _container.Resolve<CommandSummonAttacker>();
            command.Summon(user, attackerType);
        }

        public void CreateTower(UserObject user, int number, TowerType towerType)
        {//
            if (teamManager.Existed(user.userId) == Team.None)
            {
                Debug.LogError("请先选择阵营!!");
                return;
            }

            //判断用户是否已经有塔了
            if (towerManager.GetTower(user.userId) != null)
            {
                Debug.LogError("你已经有塔了");
                return;
            }

            var command = _container.Resolve<CommandBuildTower>();
            command.CreateTower(user, number, towerType);
        }

        public void CreateSuperTower(UserObject user)
        {
            if (teamManager.Existed(user.userId) == Team.None)
            {
                Debug.LogError("请先选择阵营");
                return;
            }

            var command = _container.Resolve<CommandBuildTower>();
            command.CreateSuperTower(user);
        }

        public void SpeedupTower(UserObject user, float k)
        {
            if (teamManager.Existed(user.userId) == Team.None)
            {
                Debug.LogError("请先选择阵营");
                return;
            }

            var command = _container.Resolve<CommandSpeedUpTower>();
            command.Speedup(user, k);
        }

        public void UpgradeTower(UserObject user)
        {
            var tower = towerManager.GetTower(user.userId);
            if (tower == null)
            {
                Debug.LogError("没有找到塔 " + user.nickName);
                return;
            }

            if (tower.isAtMaxLevel)
            {
                Debug.LogError("已经满级 " + user.nickName);
                return;
            }

            tower.UpgradeTower();
        }


        public void DoLike(UserObject user, int count)
        {
            var team = teamManager.Existed(user.userId);
            if (team == Team.None)
            {
                Debug.LogError("请先选择阵营");
                return;
            }

            if (team == Team.Left)
            {
                for (int i = 0; i < count; i++)
                {
                    Summon(user, 1);
                }
                //出兵
                return;
            }

            if (team == Team.Right)
            {
                //加速0.5
                var command = _container.Resolve<CommandSpeedUpTower>();
                command.Speedup(user, 0.5f);
                return;
            }

        }

        public void DoGift(UserObject user, string giftId, int count)
        {
            count = count * 10;
            var team = teamManager.Existed(user.userId);
            if (team == Team.None)
            {
                Debug.LogError("请先选择阵营");
                return;
            }

            if (team == Team.Left)
            {
                //判断礼物id
                switch (giftId)
                {
                    case "463": //小心心
                        {
                            for (int i = 0; i < count; i++)
                            {
                                Summon(user, 5);
                            }

                        }
                        break;
                    case "2002":
                        {
                            var command = _container.Resolve<CommandSummonAttacker>();
                            for (int i = 0; i < count; i++)
                            {
                                Summon(user, 6);
                            }
                        }
                        break;
                    case "3621":
                        {
                            var command = _container.Resolve<CommandSummonAttacker>();
                            for (int i = 0; i < count; i++)
                            {
                                Summon(user, 7);
                            }
                        }
                        break;
                }

                //出兵
                return;
            }

            if (team == Team.Right)
            {
                switch (giftId)
                {
                    case "463": //小心心
                        {
                            SpeedupTower(user, 1f);
                        }
                        break;
                    case "2002": //升级
                        {
                            UpgradeTower(user);
                        }
                        break;
                    case "3621":
                        {
                            CreateSuperTower(user);
                        }
                        break;
                }


                return;
            }
        }

    }
}
