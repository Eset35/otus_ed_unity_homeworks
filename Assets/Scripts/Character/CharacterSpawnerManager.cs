using UnityEngine;

namespace ShootEmUp
{
    public class CharacterSpawnerManager : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterPrefab;
        [SerializeField] private CharacterDeathObserver _characterDeathObserver;
        
        [SerializeField] private Transform _characterSpawnPosition;
        [SerializeField] private Transform _worldPosition;
        
        public void SpawnCharacter()
        {
            CharacterController character = Instantiate(this._characterPrefab);
            character.transform.parent = this._worldPosition.gameObject.transform;
            character.transform.position = this._characterSpawnPosition.transform.position;
            this._characterDeathObserver.AddCharacter(character);
        }
    }
}
