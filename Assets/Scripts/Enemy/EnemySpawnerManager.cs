using System.Collections.Generic;
using ShootEmUp.Common.Pool;
using UnityEngine;

namespace ShootEmUp.Enemy
{
    public sealed class EnemySpawnerManager : MonoBehaviour
    {
        [SerializeField] private EnemyDeathObserver _enemyDeathObserver;
        [SerializeField] private List<Transform> _enemiesSpawnPosition;
        [SerializeField] private int _maxEnemiesCounter;
        [SerializeField] private Transform _worldPosition;
        [SerializeField] private EnemyController _enemyPrefab;
        
        private int _enemiesCounter;

        public void SpawnEnemy(uint enemyNumber = 6)
        {
            for (int i = 0; i < enemyNumber; i++)
            {
                SpawnEnemy();
            }
        }
        
        private void SpawnEnemy()
        {
            if (this._enemiesCounter >= this._maxEnemiesCounter)
            {
                return;
            }

            EnemyController enemy = UnifiedPool.GetObject<EnemyController>(() => Instantiate(_enemyPrefab), enemy =>
            {
                enemy.gameObject.SetActive(true);
                return null;
            });

            this._enemiesCounter++;
            enemy.transform.SetParent(this._worldPosition);

            int index = Random.Range(0, this._enemiesSpawnPosition.Count);
            enemy.transform.position = this._enemiesSpawnPosition[index].position;
            
            this._enemyDeathObserver.AddEnemy(enemy);
            
            enemy.StartAttack();
        }
        
        public void DespawnEnemy(EnemyController enemyController)
        {
            UnifiedPool.ReleaseObj<EnemyController>(enemyController, enemy=>
            {
                enemy.gameObject.SetActive(false);
                return null;
            });
            
            this._enemiesCounter--;
            SpawnEnemy();
        }
    }
}
