using System;

namespace HiplayGame
{
    public class DebugGameNetwork : IMessageReciever
    {
        public event Action<string> onMessageRecieved;

        public void Connect()
        {
            //throw new NotImplementedException();
        }

        public void Notify(string message)
        {
            onMessageRecieved?.Invoke(message);
        }
    }

}
