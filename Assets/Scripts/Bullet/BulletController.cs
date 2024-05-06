using ShootEmUp.Common.Physics;
using ShootEmUp.Components;
using ShootEmUp.GameCycle.Models;
using UnityEngine;

namespace ShootEmUp.Bullet
{
    [RequireComponent(typeof(MoveComponent))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public sealed class BulletController : MonoBehaviour, IGameTickableListener
    {
        public struct Args
        {
            public Vector2 Position { get; set; }
            public Vector2 TargetDirection { get; set; }
            public float Speed { get; set; }
            public Color Color { get; set; }
            public PhysicsLayer PhysicsLayer { get; set; }
            public int Damage { get; set; }
        }
        
        private int _damage;
        private Vector2 _targetDirection;
        private float _speed;
        [SerializeField] private MoveComponent _moveComponent;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Rigidbody2D _rigidbody2D; 
        private BulletSpawnerManager _bulletSpawnerManager;
        
        
        public void Init(Args args)
        {
            IGameTickListener.Register(this);
            this._spriteRenderer.color = args.Color;
            this.transform.position = args.Position;
            this.gameObject.layer = (int)args.PhysicsLayer;
            this._damage = args.Damage;
            this._speed = args.Speed;
            this._targetDirection = args.TargetDirection;
            this._bulletSpawnerManager = FindObjectOfType<BulletSpawnerManager>();
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.TryGetComponent(out HitPointComponent hitPointsComponent))
            {
                return;
            }
            
            hitPointsComponent.TakeDamage(_damage);
            this._bulletSpawnerManager.DespawnBullet(this);
        }

        public void Tick()
        {
            this._moveComponent.Move(_targetDirection, _speed);
        }
    }
}