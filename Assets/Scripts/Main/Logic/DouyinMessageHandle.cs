using Adic;
using Newtonsoft.Json;
using UnityEngine;

namespace HiplayGame
{

    public class UserObject
    {
        public string method;
        public string roomId;
        public string describe;
        public string userId;
        public string nickName;
        public string content;
        public string giftId;
        public string iconUrl;
        public string count; //点赞数量
        public string comboCount; //送礼连击数

    }

    public class DouyinMessageHandle
    {
        [Inject]
        CommandManager commandManager;

        public void OnMessage(string message)
        {
            Debug.Log(message);

            var userObject = JsonConvert.DeserializeObject<UserObject>(message);

            if (userObject.method == "WebcastChatMessage") //评论
            {
                switch (userObject.content)
                {
                    case "666":
                        commandManager.JoinTeam(userObject, Team.Left);
                        break;
                    default:
                        {
                            int result;
                            if (int.TryParse(userObject.content, out result))
                            {
                                if (result > 16 || result < 1)
                                    return;

                                commandManager.JoinTeam(userObject, Team.Right);
                                commandManager.CreateTower(userObject, result, TowerType.MachineGunTower);
                            }
                        }
                        break;
                }

            }
            else if (userObject.method == "WebcastLikeMessage") //点赞
            {
                int count;
                if (int.TryParse(userObject.count, out count))
                {
                    commandManager.DoLike(userObject, count);
                }
                else
                {
                    commandManager.DoLike(userObject, 1);
                }

            }
            else if (userObject.method == "WebcastGiftMessage")
            {
                var id = userObject.giftId;
                int count;

                if (int.TryParse(userObject.comboCount, out count))
                {
                    commandManager.DoGift(userObject, userObject.giftId, count);
                }
                else
                {
                    commandManager.DoGift(userObject, userObject.giftId, 1);
                }



            }
        }
    }

}
