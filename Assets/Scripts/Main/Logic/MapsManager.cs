using System.Collections.Generic;
using Adic;
using Adic.Container;
using UnityEngine;

namespace HiplayGame
{
    public class MapsManager
    {
        [Inject]
        DataGridGenerater dataGenerater;


        /// <summary>
        /// 房间列表 to do ，要用链表
        /// </summary>
        List<DataGrid> rooms = new List<DataGrid>();

        /// <summary>
        /// 房间总数
        /// </summary>
        public int RoomsCount => rooms.Count;

        int curRoom = 0;

        /// <summary>
        /// 获取当前格子
        /// </summary>
        /// <returns></returns>
        public DataGrid GetCurrentMap()
        {
            if (curRoom >= rooms.Count)
                return null;

            return rooms[curRoom];
        }



        public void MoveToNextMap()
        {
            
        }

        /// <summary>
        /// 创建地图
        /// </summary>
        /// <returns></returns>
        public List<DataGrid> CreateMaps()
        {
            rooms = new List<DataGrid>();
            var data = dataGenerater.Generater(11, 11);
            rooms.Add(data);
            return rooms;
        }
    }
}
