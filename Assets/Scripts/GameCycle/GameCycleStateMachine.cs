using System.Collections.Generic;
using ShootEmUp.GameCycle.Models;
using ShootEmUp.GameCycle.States;
using UnityEngine;

namespace ShootEmUp.GameCycle
{
    public sealed class GameCycleStateMachine : MonoBehaviour
    {
        private readonly List<IGameStartListener> _startList = new List<IGameStartListener>();
        private readonly List<IGamePauseListener> _pauseList = new List<IGamePauseListener>();
        private readonly List<IGameResumeListener> _resumeList = new List<IGameResumeListener>();
        private readonly List<IGameEndListener> _endList = new List<IGameEndListener>();

        protected GameCycleStateMachineBaseState _state { get; private set; }

        public void SetState<T>(T state) where T : GameCycleStateMachineBaseState
        {
            this._state = state;
            this._state.StartState();
        }

        public void Awake()
        {
            IGameEventListener.OnRegister += OnRegister;
            SetState(new GameCycleStateMachineInitialState(this));
        }

        private void OnRegister(IGameEventListener listener)
        {
            if (listener is IGameStartListener)
            {
                this._startList.Add(listener as IGameStartListener);
            }

            if (listener is IGamePauseListener)
            {
                this._pauseList.Add(listener as IGamePauseListener);
            }

            if (listener is IGameResumeListener)
            {
                this._resumeList.Add(listener as IGameResumeListener);
            }

            if (listener is IGameEndListener)
            {
                this._endList.Add(listener as IGameEndListener);
            }
        }

        public void StartGame()
        {
            this._state.StartGame(this._startList);
        }

        public void EndGame()
        {
            this._state.EndGame(this._endList);
        }

        public void PauseGame()
        {
            this._state.PauseGame(this._pauseList);
        }

        public void ResumeGame()
        {
            this._state.ResumeGame(this._resumeList);
        }
    }
}