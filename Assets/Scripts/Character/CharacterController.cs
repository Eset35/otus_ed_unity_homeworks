using System;
using JetBrains.Annotations;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(MoveComponent))]
    [RequireComponent(typeof(HitPointComponent))]
    [RequireComponent(typeof(HealthComponent))]
    [RequireComponent(typeof(WeaponComponent))]
    public sealed class CharacterController : MonoBehaviour
    {
        private AbstractInputListener _inputManager; 
        
        private MoveComponent _moveComponent;
        private HitPointComponent hitPointComponent;
        private HealthComponent _healthComponent;
        private WeaponComponent _weaponComponent;

        public Action OnKilled;
        
        private void Start()
        {
            _inputManager = FindObjectOfType<AbstractInputListener>();
            
            _moveComponent = gameObject.GetComponent<MoveComponent>();
            hitPointComponent = gameObject.GetComponent<HitPointComponent>();
            _healthComponent = gameObject.GetComponent<HealthComponent>();
            _weaponComponent = gameObject.GetComponent<WeaponComponent>();
            
            _inputManager.OnDirectionInput += Move;
            _inputManager.OnShootInput += OnShoot;
            hitPointComponent.OnGetHit += OnGetHit;
            _healthComponent.OnDead += OnDeath;
        }

        public void OnDestroy()
        {
            _inputManager.OnDirectionInput -= Move;
            _inputManager.OnShootInput -= OnShoot;
            hitPointComponent.OnGetHit -= OnGetHit;
            _healthComponent.OnDead -= OnDeath;
        }

        private void Move(DirectionTypeEnum directionTypeEnum)
        {
            if (directionTypeEnum == DirectionTypeEnum.Left)
            {
                Vector2 dirVector = new Vector2(-1, 0) * Time.fixedDeltaTime;
                _moveComponent.MoveByRigidbodyVelocity(dirVector);
            }
            else
            {
                Vector2 dirVector = new Vector2(1, 0) * Time.fixedDeltaTime;
                _moveComponent.MoveByRigidbodyVelocity(dirVector);
            }
        }

        private void OnGetHit(int damage)
        {
            _healthComponent.TakeDamage(damage);
        }

        private void OnShoot()
        {
            _weaponComponent.Shoot(Vector2.up);
        }

        private void OnDeath()
        {
            OnKilled?.Invoke();
            Destroy(this.gameObject);
        }
    }
}