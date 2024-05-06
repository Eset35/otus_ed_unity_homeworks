using System;
using UnityEngine;

namespace ShootEmUp.Components
{
    public sealed class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int _health;
        private int _currentHealth;

        private void Awake()
        {
            this._currentHealth = this._health;
        }

        public event Action OnDead;

        public void Reset()
        {
            this._currentHealth = this._health;
        }

        public void TakeDamage(int damage)
        {
            this._currentHealth -= damage;

            if (this._currentHealth < 0)
            {
                this.OnDead?.Invoke();
            }
        }
    }
}