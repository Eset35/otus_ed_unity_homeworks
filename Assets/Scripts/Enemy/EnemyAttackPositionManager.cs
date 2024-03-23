using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ShootEmUp
{
    public sealed class EnemyAttackPositionManager : MonoBehaviour
    {
        [SerializeField] private Transform[] _attackPositions;
        private List<Transform> _attackPositionsList;

        private void Start()
        {
            _attackPositionsList = _attackPositions.ToList();
        }

        public void FreeAttackPosition(Transform attackPosition)
        {
            _attackPositionsList.Add(attackPosition);
        }
        
        public bool TryGetRandomAttackPosition(out Transform attackPosition)
        {
            if (_attackPositionsList.Count > 0)
            {
                var index = Random.Range(0, this._attackPositionsList.Count);
                attackPosition = this._attackPositionsList[index];
                this._attackPositionsList.RemoveAt(index);
                return true;
            }

            attackPosition = null;
            return false;
        }
    }
}