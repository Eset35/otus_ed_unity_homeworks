using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemySpawnerManager : MonoBehaviour
    {
        [SerializeField] private EnemyDeathObserver _enemyDeathObserver;
        [SerializeField] private List<Transform> _enemiesSpawnPosition;
        [SerializeField] private int _maxEnemiesCounter;
        [SerializeField] private PoolContainer _enemyContainer;
        [SerializeField] private Transform _worldPosition;
        
        private int _enemiesCounter;

        public void SpawnEnemy(uint enemyNumber = 6)
        {
            for (int i = 0; i < enemyNumber; i++)
            {
                SpawnEnemy();
            }
        }
        
        public void SpawnEnemy()
        {
            if (this._enemiesCounter >= this._maxEnemiesCounter)
            {
                return;
            }

            EnemyController enemy = this._enemyContainer.Get().GetComponent<EnemyController>();
            this._enemiesCounter++;
            enemy.transform.SetParent(this._worldPosition);

            int index = Random.Range(0, this._enemiesSpawnPosition.Count);
            enemy.transform.position = this._enemiesSpawnPosition[index].position;
            
            this._enemyDeathObserver.AddEnemy(enemy);
            
            enemy.StartAttack();
        }
        
        public void DespawnEnemy(EnemyController enemyController)
        {
            this._enemyContainer.Delete(enemyController.GetComponent<PoolObject>());
            this._enemiesCounter--;
            SpawnEnemy();
        }
    }
}
