using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyAttackPositionManager : MonoBehaviour
    {
        [SerializeField] private Transform[] _attackPositions;
        private List<Transform> _attackPositionsList;
        
        public void FreeAttackPosition(Transform attackPosition)
        {
            this._attackPositionsList.Add(attackPosition);
        }
        
        public bool TryGetRandomAttackPosition(out Transform attackPosition)
        {
            if (this._attackPositionsList == null)
            {
                this._attackPositionsList = this._attackPositions.ToList();
            }
            
            if (this._attackPositionsList.Count > 0)
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