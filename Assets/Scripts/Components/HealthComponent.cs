using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class HealthComponent : MonoBehaviour
    {
        [SerializeField]
        private int _health;

        public event Action OnDead;
        
        public void TakeDamage(int damage)
        {
            this._health -= damage;

            if (this._health < 0)
            {
                this.OnDead?.Invoke();
            }
        }
    }
}