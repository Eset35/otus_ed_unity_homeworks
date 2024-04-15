using System;
using UnityEngine;

namespace ShootEmUp.Components
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