using ShootEmUp;
using UnityEngine;

public sealed class EnemyDeathObserver : MonoBehaviour
{
    [SerializeField]
    private EnemySpawnerManager _enemySpawnerManager;

    public void AddEnemy(EnemyController enemyController)
    {
        enemyController.OnKilled += OnEnemyDeath;
    }

    public void OnEnemyDeath(EnemyController enemyController)
    {
        enemyController.OnKilled -= OnEnemyDeath;
        this._enemySpawnerManager.DespawnEnemy(enemyController);
    }
}
