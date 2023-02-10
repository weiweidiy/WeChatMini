/****************************************************
    文件：MoveButton.cs
    作者：嵇春苇
    邮箱: 49595272@qq.com
    日期：2022/12/22 23:20:42
    功能：Nothing
*****************************************************/


using System.Collections;
using System.Collections.Generic;
using Adic;
using UnityEngine;

namespace HiplayGame
{

    /// <summary>
    /// 格子地图调试器
    /// </summary>
    public class DataGridDebugger : MonoBehaviour
    {
        [Inject]
        MapsManager mapManager;

        private void Start()
        {
            //this.Inject();
        }

        private void OnGUI()
        {
            var data = mapManager.GetCurrentMap();

            var size = 50;

            GUIStyle style = new GUIStyle();
            style.fontSize = 30;

            for (int x = 0; x < data.Width; x ++)
            {
                for(int y = 0; y < data.Height; y ++)
                {
                    var cellData = data.GetData(x, y);
                    GUI.TextArea(new Rect(size * x, Screen.height - size * y - 100 , size, size), cellData.value.ToString(), style);
                }
            }
            
        }

        private void OnDrawGizmos()
        {
            
        }


    }
}