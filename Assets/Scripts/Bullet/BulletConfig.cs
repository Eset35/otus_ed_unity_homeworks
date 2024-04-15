using ShootEmUp.Common.Physics;
using UnityEngine;

namespace ShootEmUp.Bullet
{
    [CreateAssetMenu(
        fileName = "BulletConfig",
        menuName = "Bullets/New BulletConfig"
    )]
    public sealed class BulletConfig : ScriptableObject
    { 
        [SerializeField]
        private PhysicsLayer _physicsLayer;

        public PhysicsLayer PhysicsLayer => _physicsLayer;
        
        
        [SerializeField]
        private Color _color;

        public Color Color => _color;
        
        [SerializeField]
        private int _damage;

        public int Damage => _damage;

        [SerializeField]
        private float _speed;

        public float Speed => _speed;
    }
}