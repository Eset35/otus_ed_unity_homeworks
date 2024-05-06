using ShootEmUp.Common;
using ShootEmUp.GameCycle.Models;
using UnityEngine;

namespace ShootEmUp.Character
{
    public class CharacterSpawnerManager : MonoBehaviour, IGameStartListener, IGameEndListener
    { 
        [SerializeField] private PlayerCharacterController _playerCharacterPrefab;
        [SerializeField] private CharacterDeathObserver _characterDeathObserver;
        
        [SerializeField] private Transform _characterSpawnPosition;
        [SerializeField] private Transform _worldPosition;

        private PlayerCharacterController _playerCharacter = null;

        private void Start()
        {
            IGameEventListener.Register(this);
        }
        
        private void SpawnCharacter()
        {
            if (this._playerCharacter == null)
            {
                this._playerCharacter = Instantiate(this._playerCharacterPrefab, this._worldPosition.gameObject.transform, true);
            }
            
            this._playerCharacter.gameObject.SetActive(true);
            this._playerCharacter.Init();
            this._playerCharacter.transform.position = this._characterSpawnPosition.transform.position;
            this._characterDeathObserver.AddCharacter(this._playerCharacter);
        }

        private void DespawnCharacter()
        {
            this._playerCharacter.DeInit();
            this._playerCharacter.gameObject.SetActive(false);
        }

        public void OnGameStart()
        {
            this.SpawnCharacter();
        }

        public void OnGameEnd()
        {
            this.DespawnCharacter();
        }
    }
}
