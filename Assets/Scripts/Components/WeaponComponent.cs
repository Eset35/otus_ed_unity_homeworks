using UnityEngine;

namespace ShootEmUp
{
    public sealed class WeaponComponent : MonoBehaviour
    {
        public Vector2 Position
        {
            get { return this.firePoint.position; }
        }

        public Quaternion Rotation
        {
            get { return this.firePoint.rotation; }
        }

        private BulletSpawnerManager _bulletSpawnerManager;
        [SerializeField] private BulletConfig _bulletConfig;
        [SerializeField] private Transform firePoint;
        
        private void Start()
        {
            this._bulletSpawnerManager = FindObjectOfType<BulletSpawnerManager>();
        }

        public void Shoot(Vector2 direction)
        {
            this._bulletSpawnerManager.Spawn(this._bulletConfig, gameObject.transform, direction);
        }
    }
}