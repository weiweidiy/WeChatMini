using System;
using System.Collections.Generic;

namespace HiplayGame
{
    /// <summary>
    /// 用户管理
    /// </summary>
    public class UserManager
    {
        public event Action<UserObject> onUserAdded;

        Dictionary<string, UserObject> users = new Dictionary<string, UserObject>();

        public void AddUser(UserObject user)
        {
            if (!users.ContainsKey(user.userId))
            {
                users.Add(user.userId, user);
                onUserAdded?.Invoke(user);
            }

        }

        public void RemoveUser(UserObject user)
        {
            var id = user.userId;
            if (users.ContainsKey(id))
                users.Remove(id);
        }

        public bool Exist(UserObject user)
        {
            return users.ContainsKey(user.userId);
        }

        public void Clear()
        {
            users.Clear();
        }
    }

}
