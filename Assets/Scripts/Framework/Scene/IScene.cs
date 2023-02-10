using System;
using Cysharp.Threading.Tasks;

namespace HiplayGame
{
    public interface IScene
    {

        string Name { get; }

        string Location { get; }

        /// <summary>
        /// 初始化
        /// </summary>
        void Initialize();

        /// <summary>
        /// �������� ��start)
        /// </summary>
        UniTask OnEnter();

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
