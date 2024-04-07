using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class PoolContainer : MonoBehaviour
    {
        private readonly Stack<PoolObject> _pool = new Stack<PoolObject>(25);
        [SerializeField] private GameObject _prefab;

        public PoolObject Get()
        {
            if (this._prefab == null)
            {
                return null;
            }

            PoolObject poolObject;

            if (this._pool.Count > 0)
            {
                poolObject = this._pool.Pop();
            }
            else
            {
                GameObject go = Instantiate(this._prefab);
                go.transform.SetParent(this.gameObject.transform);
                
                poolObject = go.AddComponent<PoolObject>();
                poolObject.SetPool(this, this.gameObject.transform.position, this.gameObject.transform.rotation);
            }

            poolObject.SetActive(true);
            return poolObject;
        }

        public void Delete(PoolObject poolObject)
        {
            if (poolObject != null && poolObject.IsThisPool(this))
            {
                poolObject.SetActive(false);
                poolObject.transform.SetParent(this.gameObject.transform);
                if (!this._pool.Contains(poolObject))
                {
                    this._pool.Push(poolObject);
                }
            }
        }
    }
}