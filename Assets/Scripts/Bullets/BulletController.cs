using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public sealed class BulletController : MonoBehaviour
    {
        public struct Args
        {
            public Vector2 Position { get; set; }
            public Vector2 Velocity { get; set; }
            public Color Color { get; set; }
            public PhysicsLayer PhysicsLayer { get; set; }
            public int Damage { get; set; }
        }
        
        private int _damage;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private BulletSpawnerManager _bulletSpawnerManager;
        
        public void Init(Args args)
        {
            this._spriteRenderer.color = args.Color;
            this.transform.position = args.Position;
            this.gameObject.layer = (int)args.PhysicsLayer;
            this._rigidbody2D.velocity = args.Velocity;
            this._damage = args.Damage;

            this._bulletSpawnerManager = FindObjectOfType<BulletSpawnerManager>();
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out HitPointComponent hitPointsComponent))
            {
                hitPointsComponent.TakeDamage(_damage);
                this._bulletSpawnerManager.DespawnBullet(this);
            }
        }
    }
}