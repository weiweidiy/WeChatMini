using System;
using System.Collections.Generic;
using System.Linq;

namespace HiplayGame
{

    public class TeamManager
    {


        public event Action<UserObject, Team> onMemberAdded;
        public event Action<UserObject, Team> onMemberExited;

        List<UserObject> leftTeam = new List<UserObject>();

        List<UserObject> rightTeam = new List<UserObject>();

        /// <summary>
        /// 加入一个队伍
        /// </summary>
        /// <param name="team"></param>
        /// <param name="id"></param>
        public void JoinTeam(Team team, UserObject user)
        {
            string id = user.userId;
            var hasTeam = Existed(id);
            if (hasTeam != Team.None) //有队伍
            {
                if (hasTeam != team)  //不同队伍
                {
                    ExitTeam(id);     //退出当前队伍
                }
                else
                {
                    return;
                }
            }

            GetTeamMembers(team).Add(user);
            onMemberAdded?.Invoke(user, team);
        }

        /// <summary>
        /// 退出队伍
        /// </summary>
        /// <param name="id"></param>
        public void ExitTeam(string id)
        {
            var hasTeam = Existed(id);
            if (hasTeam == Team.None)
                return;

            var user = GetUser(hasTeam, id);
            var teamMembers = GetTeamMembers(hasTeam);
            if (teamMembers.Remove(user))
            {
                onMemberExited?.Invoke(user, hasTeam);
            }
        }

        public UserObject GetUser(Team team, string id)
        {
            var members = GetTeamMembers(team);
            var user = members.Where((p) => p.userId.Equals(id)).SingleOrDefault();
            if (user != null)
            {
                return user;
            }
            return null;
        }

        public List<UserObject> GetTeamMembers(Team team)
        {
            switch (team)
            {
                case Team.Left:
                    return leftTeam;
                case Team.Right:
                    return rightTeam;
            }
            return null;
        }

        public bool Existed(Team team, string id)
        {
            var lst = GetTeamMembers(team).Where((p) => p.userId.Equals(id)).ToList();
            if (lst.Count > 0)
                return true;

            return false;
        }

        public Team Existed(string id)
        {
            var lst = GetTeamMembers(Team.Left).Where((p) => p.userId.Equals(id)).ToList();
            if (lst.Count > 0)
                return Team.Left;

            var rLst = GetTeamMembers(Team.Right).Where((p) => p.userId.Equals(id)).ToList();
            if (rLst.Count > 0)
                return Team.Right;

            return Team.None;
        }

        public void Clear()
        {
            leftTeam.Clear();
            rightTeam.Clear();

            onMemberExited?.Invoke(null, Team.Left);
            onMemberExited?.Invoke(null, Team.Right);
        }
    }

}
