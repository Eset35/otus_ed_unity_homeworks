using ShootEmUp.Common;
using ShootEmUp.Common.Pool;
using ShootEmUp.GameCycle.Models;
using UnityEngine;

namespace ShootEmUp.Bullet
{
    public sealed class BulletSpawnerManager : MonoBehaviour, IGameEndListener
    {
        [SerializeField] private Transform _worldTransform;
        [SerializeField] private BulletController _bulletPrefab;
        
        private void Start()
        {
            IGameEventListener.Register(this);
        }
        
        public void Spawn(BulletConfig bulletConfig, Transform transform, Vector2 direction)
        {
            BulletController bulletController = UnifiedPool.GetObject<BulletController>(
                () => Instantiate(_bulletPrefab),
                bullet =>
                {
                    bullet.gameObject.SetActive(true);
                    return null;
                });
            
            bulletController.gameObject.transform.parent = _worldTransform;
            
            BulletController.Args args = new BulletController.Args()
            {
                PhysicsLayer = bulletConfig.PhysicsLayer,
                Position = transform.position,
                TargetDirection = direction,
                Speed = bulletConfig.Speed,
                Color = bulletConfig.Color,
                Damage = bulletConfig.Damage,
            };
            
            bulletController.Init(args);
        }

        public void DespawnBullet(BulletController bulletController)
        {
            UnifiedPool.ReleaseObj(bulletController, bullet =>
            {
                bullet.gameObject.SetActive(false);
                return null;
            });
        }

        public void OnGameEnd()
        {
            BulletController[] bullets = GameObject.FindObjectsOfType<BulletController>();
        
            foreach (var bullet in bullets)
            {
                this.DespawnBullet(bullet);
            }
        }
    }
}
