using UnityEngine;

namespace ShootEmUp.Enemy
{
    public sealed class EnemyDeathObserver : MonoBehaviour
    {
        [SerializeField] private EnemySpawnerManager _enemySpawnerManager;

        public void AddEnemy(EnemyController enemyController)
        {
            enemyController.OnKilled += OnEnemyDeath;
        }

        private void OnEnemyDeath(EnemyController enemyController)
        {
            enemyController.OnKilled -= OnEnemyDeath;
            this._enemySpawnerManager.DespawnEnemy(enemyController);
            this._enemySpawnerManager.SpawnEnemy(1);
        }
    }
}
