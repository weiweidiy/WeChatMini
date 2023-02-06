/****************************************************
    文件：BtnStartGame.cs
    作者：嵇春苇
    邮箱: 49595272@qq.com
    日期：2022/12/22 17:27:23
    功能：Nothing
*****************************************************/


using Adic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace hiplaygame
{

    public class StartButton : MonoBehaviour
    {
        public event Action onClicked;

        [Inject]
        public void Initialize()
        {
            var btn = GetComponent<Button>();
            btn.onClick.AddListener(() =>
            {
                onClicked?.Invoke();
            });
        }
        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("StartButton start");
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}