using System.Collections.Generic;
using ShootEmUp.GameCycle.Models;

namespace ShootEmUp.GameCycle.States
{
    public sealed class GameCycleStateMachinePauseState : GameCycleStateMachineBaseState
    {
        public GameCycleStateMachinePauseState(GameCycleStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void ResumeGame(List<IGameResumeListener> listeners)
        {
            base.ResumeGame(listeners, new object());
            this._stateMachine.SetState(new GameCycleStateMachineInProgressState(this._stateMachine));
        }

        public override void EndGame(List<IGameEndListener> listeners)
        {
            base.EndGame(listeners, new object());
            this._stateMachine.SetState(new GameCycleStateMachineEndGameState(this._stateMachine));
        }
    }
}