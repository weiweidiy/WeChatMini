namespace HiplayGame
{
    public interface IScene
    {
        string Name { get; }

        string Location { get; }

        /// <summary>
        /// �������� ��start)
        /// </summary>
        void OnEnter();

        /// <summary>
        /// ������ɽ���(���û�й��ɣ��򲻻����)
        /// </summary>
        void OnEnterTransitionComplete();

        /// <summary>
        /// �˳����ɿ�ʼʱ
        /// </summary>
        void OnExitTransitionStart();

        /// <summary>
        /// �����˳���destory)
        /// </summary>
        void OnExit();    
        
    }
}
