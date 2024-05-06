using System;
using ShootEmUp.Components;
using ShootEmUp.Character;
using UnityEngine;
using ShootEmUp.GameCycle.Models;

namespace ShootEmUp.Enemy
{
    [RequireComponent(typeof(HitPointComponent))]
    [RequireComponent(typeof(HealthComponent))]
    [RequireComponent(typeof(WeaponComponent))]
    public sealed class EnemyController : MonoBehaviour, IGameFixedTickableListener
    {
        private WeaponComponent _weaponComponent;
        private HealthComponent _healthComponent;
        private HitPointComponent _hitPointComponent;
        private MoveComponent _moveComponent;

        private EnemyAttackPositionManager _attackPositionManager;
        private PlayerCharacterController _playerCharacterController;
        [SerializeField] private float _fireRate;

        private float _currentTime;

        private Transform _destination;
        private bool _isReachedAttackPosition;

        public Action<EnemyController> OnKilled;

        public void Init()
        {
            this._playerCharacterController = FindObjectOfType<PlayerCharacterController>();
            this._moveComponent = GetComponent<MoveComponent>();
            this._weaponComponent = GetComponent<WeaponComponent>();
            this._healthComponent = GetComponent<HealthComponent>();
            this._hitPointComponent = GetComponent<HitPointComponent>();
            this._healthComponent.Reset();
        }

        private void Start()
        {
            IGameFixedTickableListener.Register(this);
        }

        public void StartAttack()
        {
            this._hitPointComponent.OnGetHit += OnGetHit;
            this._healthComponent.OnDead += OnCharacterDeath;
            
            if (this._attackPositionManager == null)
            {
                this._attackPositionManager = FindObjectOfType<EnemyAttackPositionManager>();
            }
            
            if (!this._attackPositionManager.TryGetRandomAttackPosition(out Transform attackPosition))
            {
                Destroy(gameObject);
            }

            this._isReachedAttackPosition = false;
            this._destination = attackPosition;
        }

        public void DeInit()
        {
            this._hitPointComponent.OnGetHit -= OnGetHit;
            this._healthComponent.OnDead -= OnCharacterDeath;
            
            if (this._destination != null)
            {
                this._attackPositionManager.FreeAttackPosition(this._destination);
            }
        }

        private void OnGetHit(int damage)
        {
            this._healthComponent.TakeDamage(damage);
        }

        private void OnCharacterDeath()
        {
            this.OnKilled?.Invoke(this);
        }

        public void FixedTick()
        {
            if (!this._isReachedAttackPosition)
            {
                MoveToAttackPosition();
                return;
            }

            Attack();
        }

        private void Attack()
        {
            this._currentTime -= Time.fixedDeltaTime;
            if (!(this._currentTime <= 0))
            {
                return;
            }
            
            this.OnShoot();
            this._currentTime += this._fireRate;
        }
        
        private void MoveToAttackPosition()
        {
            if (this._isReachedAttackPosition || _destination == null)
            {
                return;
            }
            
            Vector2 vector = (Vector2)this._destination.transform.position - (Vector2)this.transform.position;
            if (vector.magnitude <= 0.25f)
            {
                this._isReachedAttackPosition = true;
                return;
            }

            var direction = vector.normalized * Time.fixedDeltaTime;
            this._moveComponent.Move(direction);
        }

        private void OnShoot()
        {
            var startPosition = this._weaponComponent.Position;
            var vector = (Vector2)this._playerCharacterController.gameObject.transform.position - startPosition;
            var direction = vector.normalized;
            this._weaponComponent.Shoot(direction);
        }
    }
}
