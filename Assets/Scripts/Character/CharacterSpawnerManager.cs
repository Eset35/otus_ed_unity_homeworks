using UnityEngine;

namespace ShootEmUp.Character
{
    public class CharacterSpawnerManager : MonoBehaviour
    { 
        [SerializeField] private PlayerCharacterController _playerCharacterPrefab;
        [SerializeField] private CharacterDeathObserver _characterDeathObserver;
        
        [SerializeField] private Transform _characterSpawnPosition;
        [SerializeField] private Transform _worldPosition;
        
        public void SpawnCharacter()
        {
            PlayerCharacterController playerCharacter = Instantiate(this._playerCharacterPrefab, this._worldPosition.gameObject.transform, true);
            playerCharacter.transform.position = this._characterSpawnPosition.transform.position;
            this._characterDeathObserver.AddCharacter(playerCharacter);
        }
    }
}
