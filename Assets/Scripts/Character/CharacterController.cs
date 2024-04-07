using System;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(MoveComponent))]
    [RequireComponent(typeof(HitPointComponent))]
    [RequireComponent(typeof(HealthComponent))]
    [RequireComponent(typeof(WeaponComponent))]
    public sealed class CharacterController : MonoBehaviour
    {
        private InputManager _inputManager; 
        private MoveComponent _moveComponent;
        private HitPointComponent _hitPointComponent;
        private HealthComponent _healthComponent;
        private WeaponComponent _weaponComponent;

        public Action OnKilled;
        
        private void Start()
        {
            this._inputManager = FindObjectOfType<InputManager>();
            
            this._moveComponent = this.gameObject.GetComponent<MoveComponent>();
            this._hitPointComponent = this.gameObject.GetComponent<HitPointComponent>();
            this._healthComponent = this.gameObject.GetComponent<HealthComponent>();
            this._weaponComponent = this.gameObject.GetComponent<WeaponComponent>();
            
            this._inputManager.OnDirectionInput += Move;
            this._inputManager.OnShootInput += OnShoot;
            this._hitPointComponent.OnGetHit += OnGetHit;
            this._healthComponent.OnDead += OnDeath;
        }

        public void OnDestroy()
        {
            this._inputManager.OnDirectionInput -= Move;
            this._inputManager.OnShootInput -= OnShoot;
            this._hitPointComponent.OnGetHit -= OnGetHit;
            this._healthComponent.OnDead -= OnDeath;
        }

        private void Move(DirectionTypeEnum directionTypeEnum)
        {
            this._moveComponent.Move(directionTypeEnum);
        }

        private void OnGetHit(int damage)
        {
            this._healthComponent.TakeDamage(damage);
        }

        private void OnShoot()
        {
            this._weaponComponent.Shoot(Vector2.up);
        }

        private void OnDeath()
        {
            this.OnKilled?.Invoke();
            Destroy(this.gameObject);
        }
    }
}