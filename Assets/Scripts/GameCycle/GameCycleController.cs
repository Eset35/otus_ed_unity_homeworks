using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameCycleController : MonoBehaviour
    {
        [SerializeField] private EnemySpawnerManager _enemySpawnerManager;
        [SerializeField] private CharacterSpawnerManager _characterSpawnerManager;

        private void Start()
        {
            StartGame();
        }

        private void StartGame()
        {
            this._characterSpawnerManager.SpawnCharacter();
            this._enemySpawnerManager.SpawnEnemy(6);
        }

        public void FinishGame()
        {
            Debug.Log("Game over!");
            Time.timeScale = 0;
        }
    }
}