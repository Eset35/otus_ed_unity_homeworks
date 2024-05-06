namespace ShootEmUp.GameCycle.States
{
    public class GameCycleStateMachinePreStartState : GameCycleStateMachineBaseState
    {
        public GameCycleStateMachinePreStartState(GameCycleStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void StartState()
        {
            this._stateMachine.SetState(new GameCycleStateMachineInProgressState(this._stateMachine));
        }
    }
}