using UnityEngine;

namespace ShootEmUp
{
    public sealed class WeaponComponent : MonoBehaviour
    {
        [SerializeField] private BulletConfig _bulletConfig;
        [SerializeField] private Bullet _bulletPrefab;

        public Vector2 Position
        {
            get { return this.firePoint.position; }
        }

        public Quaternion Rotation
        {
            get { return this.firePoint.rotation; }
        }

        [SerializeField] private Transform firePoint;

        public void Shoot(Vector2 direction)
        {
            Bullet bullet = Instantiate(this._bulletPrefab);
            Bullet.Args args = new Bullet.Args()
            {
                PhysicsLayer = this._bulletConfig.PhysicsLayer,
                Position = this.Position,
                Velocity = this.Rotation * direction * this._bulletConfig.Speed,
                Color = this._bulletConfig.Color,
                Damage = this._bulletConfig.Damage,
            };
            
            bullet.Init(args);
        }
    }
}