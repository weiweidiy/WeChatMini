using Adic;
using HiplayGame;
using TowerDefense.Level;
using TowerDefense.Towers;
using TowerDefense.Towers.Placement;
using TowerDefense.UI.HUD;
using UnityEngine;

public class TestTower : MonoBehaviour
{
    [Inject]
    IAssetLoader assetLoader;

    [Inject]
    TowerManager towerManager;

    [Inject]
    TeamManager teamManager;

    [Inject]
    CommandManager commandManager;

    //public GameObject[] area;
    public TowerPlacementGridManager gridManager;
    // Start is called before the first frame update
    void Start()
    {
        this.Inject();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
            var msg = new UserObject();
            msg.method = "WebcastChatMessage";
            msg.nickName = "weiwei";
            msg.userId = "11";
            msg.content = "6";
            msg.iconUrl = "https://p26.douyinpic.com/aweme/100x100/aweme-avatar/tos-cn-i-0813_d45d9f9f121d4543b4e91256ba93b792.jpeg?from=3067671334";

            commandManager.CreateTower(msg, 1, TowerType.LaserTower);
            //for (int i = 1; i < 17; i++)
            //{
            //    commandManager.CreateTower(msg, i, TowerType.LaserTower);
            //}
            //commandManager.CreateTower(msg, int.Parse(msg.content), 4);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            var msg = new UserObject();
            msg.method = "WebcastChatMessage";
            msg.nickName = "yy";
            msg.userId = "11";
            msg.content = "2";
            msg.iconUrl = "https://p26.douyinpic.com/aweme/100x100/aweme-avatar/tos-cn-i-0813_d45d9f9f121d4543b4e91256ba93b792.jpeg?from=3067671334";
            commandManager.CreateTower(msg, int.Parse(msg.content), TowerType.MachineGunTower);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            var msg = new UserObject();
            msg.method = "WebcastChatMessage";
            msg.nickName = "weiwei";
            msg.userId = "11";
            msg.content = "1";
            msg.iconUrl = "https://p26.douyinpic.com/aweme/100x100/aweme-avatar/tos-cn-i-0813_d45d9f9f121d4543b4e91256ba93b792.jpeg?from=3067671334";
            teamManager.JoinTeam(Team.Right, msg);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            var msg = new UserObject();
            msg.method = "WebcastChatMessage";
            msg.nickName = "weiwei";
            msg.userId = "11";
            msg.content = "1";
            msg.iconUrl = "https://p26.douyinpic.com/aweme/100x100/aweme-avatar/tos-cn-i-0813_d45d9f9f121d4543b4e91256ba93b792.jpeg?from=3067671334";
            teamManager.JoinTeam(Team.Left, msg);
        }



        if (Input.GetKeyDown(KeyCode.Space))
            UpgradeTower(1);

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (LevelManager.instanceExists)
            {
                LevelManager.instance.BuildingCompleted();
            }
            else
            {
                Debug.LogError("关卡不存在");
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            var msg = new UserObject();
            msg.method = "WebcastChatMessage";
            msg.nickName = "weiwei";
            msg.userId = "22";
            msg.content = "6";
            msg.iconUrl = "https://p26.douyinpic.com/aweme/100x100/aweme-avatar/tos-cn-i-0813_d45d9f9f121d4543b4e91256ba93b792.jpeg?from=3067671334";
            commandManager.Summon(msg, 1);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            var msg = new UserObject();
            msg.method = "WebcastChatMessage";
            msg.nickName = "weiwei";
            msg.userId = "11";
            msg.content = "6";
            msg.iconUrl = "https://p26.douyinpic.com/aweme/100x100/aweme-avatar/tos-cn-i-0813_d45d9f9f121d4543b4e91256ba93b792.jpeg?from=3067671334";
            commandManager.CreateSuperTower(msg);

        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            var msg = new UserObject();
            msg.method = "WebcastChatMessage";
            msg.nickName = "weiwei";
            msg.userId = "11";
            msg.content = "6";
            msg.iconUrl = "https://p26.douyinpic.com/aweme/100x100/aweme-avatar/tos-cn-i-0813_d45d9f9f121d4543b4e91256ba93b792.jpeg?from=3067671334";
            commandManager.SpeedupTower(msg, 2);
        }
    }

    void CreateTower(int number)
    {
        var area = gridManager.GetPlacementTile(number);
        var placement = area.Item1;
        var tile = area.Item2;
        var vec = area.Item3;

        if (tile.tileState == PlacementTileState.Filled)
        {
            Debug.LogError("已经有塔了");
            return;
        }
        //var placement = grid.GetComponent<IPlacementArea>();
        var pos = placement.GridToWorld(vec, new Core.Utilities.IntVector2(1, 1));

        assetLoader.InstantiateAsync("MachineGunTower", null, false, (go) =>
        {
            var tower = go.GetComponent<Tower>();
            tower.Initialize(placement, vec, "1111", "不是黄蓉");
            go.transform.position = pos;

            towerManager.AddTower(number, tower);

        });
    }

    void UpgradeTower(int number)
    {
        var tower = towerManager.GetTower(number);
        if (tower == null)
        {
            Debug.LogError("没有找到塔 " + number);
            return;
        }

        if (tower.isAtMaxLevel)
        {
            Debug.LogError("已经满级 " + number);
            return;
        }

        tower.UpgradeTower();
    }
}
