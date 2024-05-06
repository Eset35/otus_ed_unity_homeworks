using ShootEmUp.GameCycle;
using UnityEngine;

namespace ShootEmUp.Character
{
    public class CharacterDeathObserver : MonoBehaviour
    {
        [SerializeField] private GameCycleStateMachine gameCycleStateMachine;

        public void AddCharacter(PlayerCharacterController controller)
        {
            controller.OnKilled += OnCharacterDeath;
        }

        private void OnCharacterDeath()
        {
            this.gameCycleStateMachine.EndGame();
        }
    }
}
