using System;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(MoveComponent))]
    [RequireComponent(typeof(HitPointComponent))]
    [RequireComponent(typeof(HealthComponent))]
    [RequireComponent(typeof(WeaponComponent))]
    public class EnemyController : MonoBehaviour
    {
        private WeaponComponent _weaponComponent;
        private HealthComponent _healthComponent;
        private HitPointComponent hitPointComponent;
        private MoveComponent _moveComponent;

        private EnemyAttackPositionManager _attackPositionManager;
        private CharacterController _characterController;

        [SerializeField] private float _fireRate;

        private float _currentTime;

        private Transform _destination;
        private bool _isReached;

        public event Action OnKilled;

        private void Start()
        {
            _characterController = FindObjectOfType<CharacterController>();
            _moveComponent = GetComponent<MoveComponent>();
            _weaponComponent = GetComponent<WeaponComponent>();
            _healthComponent = GetComponent<HealthComponent>();
            hitPointComponent = GetComponent<HitPointComponent>();

            hitPointComponent.OnGetHit += OnGetHit;
            _healthComponent.OnDead += OnCharacterDeath;

            _attackPositionManager = FindObjectOfType<EnemyAttackPositionManager>();

            if (!_attackPositionManager.TryGetRandomAttackPosition(out Transform attackPosition))
            {
                Destroy(this.gameObject);
            }

            _destination = attackPosition;
        }

        private void OnDestroy()
        {
            hitPointComponent.OnGetHit -= OnGetHit;
            _healthComponent.OnDead -= OnCharacterDeath;

            if (_destination != null)
            {
                _attackPositionManager.FreeAttackPosition(_destination);
            }
        }

        private void OnGetHit(int damage)
        {
            _healthComponent.TakeDamage(damage);
        }

        private void OnCharacterDeath()
        {
            OnKilled?.Invoke();
            Destroy(this.gameObject);
        }

        private void FixedUpdate()
        {
            if (!this._isReached)
            {
                Vector2 vector = (Vector2)this._destination.transform.position - (Vector2)this.transform.position;
                if (vector.magnitude <= 0.25f)
                {
                    this._isReached = true;
                    return;
                }

                var direction = vector.normalized * Time.fixedDeltaTime;
                this._moveComponent.MoveByRigidbodyVelocity(direction);

                return;
            }


            this._currentTime -= Time.fixedDeltaTime;
            if (this._currentTime <= 0)
            {
                this.OnShoot();
                this._currentTime += this._fireRate;
            }
        }

        private void OnShoot()
        {
            var startPosition = this._weaponComponent.Position;
            var vector = (Vector2)this._characterController.gameObject.transform.position - startPosition;
            var direction = vector.normalized;
            this._weaponComponent.Shoot(direction);
        }
    }
}
