using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletSpawnerManager : MonoBehaviour
    {
        [SerializeField] private PoolContainer _bulletContainer;
        [SerializeField] private Transform _worldTransform;
        
        public void Spawn(BulletConfig bulletConfig, Transform transform, Vector2 direction)
        {
            BulletController bulletController = this._bulletContainer.Get().GetComponent<BulletController>();
            bulletController.gameObject.transform.parent = _worldTransform;
            
            BulletController.Args args = new BulletController.Args()
            {
                PhysicsLayer = bulletConfig.PhysicsLayer,
                Position = transform.position,
                Velocity = transform.rotation * direction * bulletConfig.Speed,
                Color = bulletConfig.Color,
                Damage = bulletConfig.Damage,
            };
            
            bulletController.Init(args);
        }

        public void DespawnBullet(BulletController bulletController)
        {
            this._bulletContainer.Delete(bulletController.GetComponent<PoolObject>());
        }
    }
}
