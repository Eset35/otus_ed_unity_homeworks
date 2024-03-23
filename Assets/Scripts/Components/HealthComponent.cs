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
            _health -= damage;

            if (_health < 0)
            {
                OnDead?.Invoke();
            }
        }
    }
}