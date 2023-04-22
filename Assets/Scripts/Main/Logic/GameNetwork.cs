using Adic;
using HiplayGame;
using System;
using UnityEngine;
using UnityWebSocket;


namespace DouyinGame
{
    public class LocalData
    {
        public string roomId;
    }

    public class GameNetwork : IMessageReciever
    {
        [Inject]
        LocalData localData;

        public event Action onOpen;
        public event Action<string> onMessageRecieved;

        WebSocket socket;
        //public void Initialize(string url, int port)
        //{
        //    string address = url + ":" + port.ToString();
        //    socket = new WebSocket(address);

        //    // ע��ص�
        //    socket.OnOpen += OnOpen;
        //    socket.OnClose += OnClose;
        //    socket.OnMessage += OnMessage;
        //    socket.OnError += OnError;
        //}


        public void Connect()
        {
            string url = "ws://127.0.0.1";
            int port = 9998;

            string address = url + ":" + port.ToString();
            socket = new WebSocket(address);

            // ע��ص�
            socket.OnOpen += OnOpen;
            socket.OnClose += OnClose;
            socket.OnMessage += OnMessage;
            socket.OnError += OnError;

            socket.ConnectAsync();
        }

        public void Disconnect()
        {
            socket.CloseAsync();
        }

        private void OnError(object sender, ErrorEventArgs e)
        {
            Debug.Log("OnError" + e.Message);
        }

        private void OnMessage(object sender, MessageEventArgs e)
        {
            Notify(e.Data);
        }

        private void OnClose(object sender, CloseEventArgs e)
        {
            Debug.Log("OnClose");
        }

        private void OnOpen(object sender, OpenEventArgs e)
        {
            Debug.Log("OnOpen " + localData.roomId);
            ((WebSocket)sender).SendAsync(localData.roomId);
        }

        public void Notify(string message)
        {
            onMessageRecieved?.Invoke(message);
        }
    }
}


//public class GameNetwork : MonoBehaviour
//{
//    // Start is called before the first frame update
//    void Start()
//    {
//        // ����ʵ��
//        string address = "ws://127.0.0.1:9998";
//        WebSocket socket = new WebSocket(address);

//        // ע��ص�
//        socket.OnOpen += OnOpen;
//        socket.OnClose += OnClose;
//        socket.OnMessage += OnMessage;
//        socket.OnError += OnError;

//        // ����
//        socket.ConnectAsync();

//        // ���� string ��������
//        //socket.SendAsync(str);

//        // ���� ���� byte[] �������ݣ�����ʹ�ã�
//        //socket.SendAsync(bytes);

//        // �ر�����
//        //socket.CloseAsync();
//    }

//    private void OnError(object sender, ErrorEventArgs e)
//    {
//        Debug.Log("OnError");
//    }

//    private void OnMessage(object sender, MessageEventArgs e)
//    {
//        Debug.Log(e.Data);
//    }

//    private void OnClose(object sender, CloseEventArgs e)
//    {
//        Debug.Log("OnClose");
//    }

//    private void OnOpen(object sender, OpenEventArgs e)
//    {
//        Debug.Log("OnOpen");
//        ((WebSocket)sender).SendAsync("7220428123983170359");
//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }
//}



