using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class HitPointComponent : MonoBehaviour
    {
        public event Action<int> OnGetHit;
        
        public void TakeDamage(int damage)
        {
            this.OnGetHit?.Invoke(damage);
        }
    }
}