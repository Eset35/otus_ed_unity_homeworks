using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp
{
    public sealed class WeaponComponent : MonoBehaviour
    {
        [SerializeField] private BulletConfig _bulletConfig;
        [FormerlySerializedAs("_bulletPrefab")] [SerializeField] private BulletController bulletControllerPrefab;

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
            BulletController bulletController = Instantiate(this.bulletControllerPrefab);
            BulletController.Args args = new BulletController.Args()
            {
                PhysicsLayer = this._bulletConfig.PhysicsLayer,
                Position = this.Position,
                Velocity = this.Rotation * direction * this._bulletConfig.Speed,
                Color = this._bulletConfig.Color,
                Damage = this._bulletConfig.Damage,
            };
            
            bulletController.Init(args);
        }
    }
}