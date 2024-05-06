using System.Collections.Generic;
using ShootEmUp.GameCycle.Models;

namespace ShootEmUp.GameCycle.States
{
    public class GameCycleStateMachineBaseState
    {
        protected readonly GameCycleStateMachine _stateMachine;

        protected GameCycleStateMachineBaseState(GameCycleStateMachine stateMachine)
        {
            this._stateMachine = stateMachine;
        }

        public virtual void StartState()
        {
        }

        protected virtual void StartGame(List<IGameStartListener> listeners, object _)
        {
            foreach (var listener in listeners)
            {
                listener.OnGameStart();
            }
        }

        protected virtual void PauseGame(List<IGamePauseListener> listeners, object _)
        {
            foreach (var listener in listeners)
            {
                listener.OnGamePause();
            }
        }

        protected virtual void ResumeGame(List<IGameResumeListener> listeners, object _)
        {
            foreach (var listener in listeners)
            {
                listener.OnGameResume();
            }
        }

        protected virtual void EndGame(List<IGameEndListener> listeners, object _)
        {
            foreach (var listener in listeners)
            {
                listener.OnGameEnd();
            }
        }

        public virtual void StartGame(List<IGameStartListener> listeners)
        {
        }

        public virtual void PauseGame(List<IGamePauseListener> listeners)
        {
        }

        public virtual void ResumeGame(List<IGameResumeListener> listeners)
        {
        }

        public virtual void EndGame(List<IGameEndListener> listeners)
        {
        }
    }
}