using System.Collections.Generic;
using ShootEmUp.Common;
using ShootEmUp.Common.Pool;
using ShootEmUp.GameCycle.Models;
using UnityEngine;

namespace ShootEmUp.Enemy
{
    public sealed class EnemySpawnerManager : MonoBehaviour, IGameStartListener, IGameEndListener
    {
        [SerializeField] private EnemyDeathObserver _enemyDeathObserver;
        [SerializeField] private List<Transform> _enemiesSpawnPosition;
        [SerializeField] private Transform _worldPosition;
        [SerializeField] private EnemyController _enemyPrefab;
        
        private void Start()
        {
            IGameEventListener.Register(this);
        }
        
        public void SpawnEnemy(uint enemyNumber = 6)
        {
            for (int i = 0; i < enemyNumber; i++)
            {
                SpawnEnemy();
            }
        }
        
        private void SpawnEnemy()
        {
            EnemyController enemy = UnifiedPool.GetObject<EnemyController>(() => Instantiate(this._enemyPrefab), enemy =>
            {
                enemy.gameObject.SetActive(true);
                return null;
            });
            
            enemy.transform.SetParent(this._worldPosition);

            int index = Random.Range(0, this._enemiesSpawnPosition.Count);
            enemy.transform.position = this._enemiesSpawnPosition[index].position;
            
            this._enemyDeathObserver.AddEnemy(enemy);
            
            enemy.Init();
            enemy.StartAttack();
        }
        
        public void DespawnEnemy(EnemyController enemyController)
        {
            UnifiedPool.ReleaseObj<EnemyController>(enemyController, enemy=>
            {
                enemy.DeInit();
                enemy.gameObject.SetActive(false);
                return null;
            });
        }

        public void OnGameStart()
        {
            this.SpawnEnemy(6);
        }

        public void OnGameEnd()
        {
            EnemyController[] enemies = GameObject.FindObjectsOfType<EnemyController>();
        
            foreach (var enemy in enemies)
            {
                DespawnEnemy(enemy);
            }
        }
    }
}
