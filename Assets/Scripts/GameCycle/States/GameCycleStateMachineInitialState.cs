using System.Collections.Generic;
using ShootEmUp.GameCycle.Models;

namespace ShootEmUp.GameCycle.States
{
    public sealed class GameCycleStateMachineInitialState : GameCycleStateMachineBaseState
    {
        public GameCycleStateMachineInitialState(GameCycleStateMachine stateMachine) : base(stateMachine)
        {
        }
        
        public override void StartGame(List<IGameStartListener> listeners)
        {
            base.StartGame(listeners, new object());
            this._stateMachine.SetState(new GameCycleStateMachinePreStartState(this._stateMachine));
        }
    }
}
