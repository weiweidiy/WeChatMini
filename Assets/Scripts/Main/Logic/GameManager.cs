using Stateless;
using System;
using UnityEngine;

namespace HiplayGame
{
    public class GameManager
    {
        public event Action onRegistStart;
        public event Action onRegistEnd;
        public event Action onReadyStart;
        public event Action onReadyEnd;
        public event Action onPlayingStart;
        public event Action onPlayingEnd;
        public event Action<bool> onResultStart;
        public event Action onResultEnd;
        enum Trigger
        {
            Regist,  //报名
            CountDown,//倒计时3秒开始
            Started,  //开始了
            Ended,    //结束了

        }

        public enum State
        {
            Initialize,
            Regist,
            Ready,
            Playing,
            Result,
            Win,
            Lose
        }

        StateMachine<State, Trigger> _machine;

        StateMachine<State, Trigger>.TriggerWithParameters<bool> _setResultTrigger;

        public GameManager()
        {
            _machine = new StateMachine<State, Trigger>(State.Initialize);

            _setResultTrigger = _machine.SetTriggerParameters<bool>(Trigger.Ended);

            _machine.Configure(State.Initialize)
                .Permit(Trigger.Regist, State.Regist);

            _machine.Configure(State.Regist)
                .OnEntry(t => OnRegistStart())
                .OnExit(t => OnRegistEnd())
                .Permit(Trigger.CountDown, State.Ready);

            _machine.Configure(State.Ready)
                .OnEntry(t => OnReadyStart())
                .OnExit(t => OnReadyEnd())
                .Permit(Trigger.Started, State.Playing);

            _machine.Configure(State.Playing)
               .OnEntry(t => OnPlayingStart())
               .OnExit(t => OnPlayingEnd())
               .Permit(Trigger.Ended, State.Result);

            _machine.Configure(State.Result)
               .OnEntryFrom(_setResultTrigger, t => OnResultStart(t))
               .OnExit(t => OnResultEnd())
               .Permit(Trigger.Regist, State.Regist);

            _machine.OnTransitioned(t => Debug.Log($"OnTransitioned: {t.Source} -> {t.Destination} via {t.Trigger}({string.Join(", ", t.Parameters)})"));
        }


        public bool IsInState(State state)
        {
            return _machine.IsInState(state);
        }

        private void OnResultEnd()
        {
            //throw new NotImplementedException();
            onResultEnd?.Invoke();
        }

        private void OnResultStart(bool re)
        {
            Debug.Log("OnResultStart " + re);
            onResultStart?.Invoke(re);
        }

        private void OnPlayingEnd()
        {
            Debug.Log("OnPlayingEnd");
            onPlayingEnd?.Invoke();
        }

        private void OnPlayingStart()
        {
            Debug.Log("OnPlayingStart");
            onPlayingStart?.Invoke();
        }

        private void OnReadyEnd()
        {

            Debug.Log("OnReadyEnd");
            onReadyEnd?.Invoke();
        }

        private void OnReadyStart()
        {
            Debug.Log("OnReadyStart");
            onReadyStart?.Invoke();
        }

        private void OnRegistEnd()
        {
            Debug.Log("OnRegistEnd");
            onRegistEnd?.Invoke();
        }

        private void OnRegistStart()
        {
            Debug.Log("OnRegistStart");
            onRegistStart?.Invoke();
        }

        /// <summary>
        /// 开始报名
        /// </summary>
        public void StartRegist()
        {
            _machine.Fire(Trigger.Regist);
        }

        /// <summary>
        /// 开始准备
        /// </summary>
        public void StartCountDown()
        {
            _machine.Fire(Trigger.CountDown);
        }

        /// <summary>
        /// 开始游戏
        /// </summary>
        public void StartPlaying()
        {
            _machine.Fire(Trigger.Started);
        }

        /// <summary>
        /// 游戏结束
        /// </summary>
        public void StartResult(bool result)
        {
            _machine.Fire(_setResultTrigger, result);
        }
    }

}
