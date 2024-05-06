using System.Collections.Generic;
using ShootEmUp.GameCycle.Models;

namespace ShootEmUp.GameCycle.States
{
    public sealed class GameCycleStateMachineInProgressState : GameCycleStateMachineBaseState
    {
        public GameCycleStateMachineInProgressState(GameCycleStateMachine stateMachine) : base(stateMachine)
        {
        }
        
        public override void PauseGame(List<IGamePauseListener> listeners)
        {
            base.PauseGame(listeners, new object());
            this._stateMachine.SetState(new GameCycleStateMachinePauseState(this._stateMachine));
        }

        public override void EndGame(List<IGameEndListener> listeners)
        {
            base.EndGame(listeners, new object());
            this._stateMachine.SetState(new GameCycleStateMachineEndGameState(this._stateMachine));
        }
    }
}
