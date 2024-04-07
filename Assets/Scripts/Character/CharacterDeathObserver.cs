using UnityEngine;

namespace ShootEmUp
{
    public class CharacterDeathObserver : MonoBehaviour
    {
        [SerializeField]
        private GameCycleController _gameCycleController;

        public void AddCharacter(CharacterController controller)
        {
            controller.OnKilled += OnCharacterDeath;
        }
        
        public void OnCharacterDeath()
        {
            this._gameCycleController.FinishGame();
        }
    }
}
