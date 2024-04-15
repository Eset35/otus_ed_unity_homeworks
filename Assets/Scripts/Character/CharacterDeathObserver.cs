using ShootEmUp.GameCycle;
using UnityEngine;

namespace ShootEmUp.Character
{
    public class CharacterDeathObserver : MonoBehaviour
    {
        [SerializeField]
        private GameCycleController _gameCycleController;

        public void AddCharacter(PlayerCharacterController controller)
        {
            controller.OnKilled += OnCharacterDeath;
        }

        private void OnCharacterDeath()
        {
            this._gameCycleController.FinishGame();
        }
    }
}
