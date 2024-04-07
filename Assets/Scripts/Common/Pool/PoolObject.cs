using UnityEngine;

namespace ShootEmUp
{
    public sealed class PoolObject : MonoBehaviour
    {
        private PoolContainer _pool;
        
        public void SetPool(PoolContainer pool, Vector3 basePosition, Quaternion baseRotation)
        {
            this.gameObject.transform.position = basePosition;
            gameObject.transform.rotation = baseRotation;
            this._pool = pool;
        }

        public void Delete()
        {
            if (_pool != null)
            {
                _pool.Delete(this);
            }
        }

        public void SetActive(bool state)
        {
            this.gameObject.SetActive(state);
        }

        public bool IsThisPool(PoolContainer container)
        {
            return _pool == container;
        }
    }
}
