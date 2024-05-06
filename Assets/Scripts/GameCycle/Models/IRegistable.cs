using System;

namespace ShootEmUp.GameCycle.Models
{
    public interface IRegistable<T>
    {
        public static event Action<T> OnRegister;

        public static void Register(T listener)
        {
            OnRegister?.Invoke(listener);
        }
    }
}