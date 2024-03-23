using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameCycleController : MonoBehaviour
    {
        [SerializeField] private EnemyController _enemyPrefab;
        [SerializeField] private List<Transform> _enemiesSpawnPosition;
        [SerializeField] private int _maxEnemiesCounter;
        
        private int _enemiesCounter;

        [SerializeField] private CharacterController _characterPrefab;
        [SerializeField] private Transform _characterSpawnPosition;

        [SerializeField] private Transform _worldPosition;

        private CharacterController _characterController;
        
        private void Start()
        {
            StartGame();
        }

        private void StartGame()
        {
            SpawnCharacter();
            
            for (int i = 0; i < _maxEnemiesCounter; i++)
            {
                SpawnEnemy();   
            }
        }

        private void SpawnCharacter()
        {
            _characterController = Instantiate(this._characterPrefab);
            _characterController.transform.parent = _worldPosition.gameObject.transform;
            _characterController.transform.position = _characterSpawnPosition.transform.position;
            _characterController.OnKilled += FinishGame;
        }
        
        private void SpawnEnemy()
        {
            if (this._enemiesCounter >= this._maxEnemiesCounter)
            {
                return;
            }
            
            EnemyController enemy = Instantiate(this._enemyPrefab);
            _enemiesCounter++;
            enemy.transform.SetParent(this._worldPosition);

            int index = Random.Range(0, _enemiesSpawnPosition.Count);
            enemy.transform.position = _enemiesSpawnPosition[index].position;

            enemy.OnKilled += EnemyWasDespawned;
        }

        private void EnemyWasDespawned()
        {
            _enemiesCounter--;
            SpawnEnemy();
        }

        private void FinishGame()
        {
            _characterController.OnKilled -= FinishGame;
            Debug.Log("Game over!");
            Time.timeScale = 0;
        }
    }
}