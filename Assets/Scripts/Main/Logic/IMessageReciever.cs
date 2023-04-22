using System;

namespace HiplayGame
{
    public interface IMessageReciever
    {
        event Action<string> onMessageRecieved;

        void Connect();

        void Notify(string message);
    }

}
