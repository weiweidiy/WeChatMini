using Adic;
using Core.Utilities;
using TowerDefense.Towers;
using TowerDefense.Towers.Placement;
using TowerDefense.UI.HUD;
using UnityEngine;

namespace HiplayGame
{
    public enum TowerType
    {
        MachineGunTower = 1,
        EnergyPylon,
        EMP,
        LaserTower,
        RocketTower,
        SuperTower
    }

    public class CommandBuildTower
    {
        [Inject]
        IAssetLoader assetLoader;

        [Inject]
        TowerManager towerManager;

        [Inject]
        TeamManager teamManager;

        public void CreateTower(UserObject user, int number, TowerType towerType)
        {

            if (!teamManager.Existed(Team.Right, user.userId))
            {
                Debug.LogError("只有防御方可以建造塔");
                return;
            }

            TowerPlacementGridManager gridManager = GameObject.Find("Level5PlacementAreas").transform.GetComponent<TowerPlacementGridManager>();

            var area = gridManager.GetPlacementTile(number);
            var placement = area.Item1;
            var tile = area.Item2;
            var vec = area.Item3;

            //判断地块是否已经有塔了
            if (tile.tileState == PlacementTileState.Filled)
            {
                Debug.LogError("位置已经有塔了");
                return;
            }


            //var placement = grid.GetComponent<IPlacementArea>();
            var pos = placement.GridToWorld(vec, new Core.Utilities.IntVector2(1, 1));


            var address = "";
            switch (towerType)
            {
                case TowerType.MachineGunTower:
                    address = "MachineGunTower";
                    break;
                case TowerType.EnergyPylon:
                    address = "EnergyPylon";
                    break;
                case TowerType.EMP:
                    address = "EMP";
                    break;
                case TowerType.LaserTower:
                    address = "LaserTower";
                    break;
                case TowerType.RocketTower:
                    address = "RocketTower";
                    break;
                case TowerType.SuperTower:
                    address = "SuperTower";
                    break;
                default:
                    address = "MachineGunTower";
                    break;
            }

            assetLoader.InstantiateAsync(address, null, false, (go) =>
            {
                var tower = go.GetComponent<Tower>();
                tower.Initialize(placement, vec, user.userId, user.nickName, user.iconUrl);
                go.transform.position = pos;

                towerManager.AddTower(number, tower);

            });
        }


        public void CreateSuperTower(UserObject user)
        {
            if (!teamManager.Existed(Team.Right, user.userId))
            {
                Debug.LogError("只有防御方可以建造塔");
                return;
            }

            TowerPlacementGridManager gridManager = GameObject.Find("Level5PlacementAreas").transform.GetComponent<TowerPlacementGridManager>();
            var vec = new IntVector2(0, 0);
            foreach (var placement in gridManager.singleGrids)
            {
                var pos = placement.GridToWorld(vec, new IntVector2(0, 0));

                assetLoader.InstantiateAsync("SuperTower", null, false, (go) =>
                {
                    var tower = go.GetComponent<Tower>();
                    tower.Initialize(placement, vec, user.userId, user.nickName, user.iconUrl);
                    go.transform.position = pos;

                });
            }
        }
    }
}
