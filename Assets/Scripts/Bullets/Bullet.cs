using System;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public sealed class Bullet : MonoBehaviour
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

        public void Init(Args args)
        {
            this._spriteRenderer.color = args.Color;
            this.transform.position = args.Position;
            this.gameObject.layer = (int)args.PhysicsLayer;
            this._rigidbody2D.velocity = args.Velocity;
            this._damage = args.Damage;
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out HitPointsComponent hitPointsComponent))
            {
                hitPointsComponent.TakeDamage(_damage);
                Destroy(this.gameObject);
            }
        }
    }
}