using System.Collections.Generic;
using System;
using System.Collections;

namespace ShootEmUp.Common.Pool
{
    public static class UnifiedPool
    {
        private static readonly Dictionary<string, ICollection> _queueDictionary = new Dictionary<string, ICollection>();

        public static T GetObject<T>(Func<T> createFunc, Func<T, object> activateAction)
        {
            string queueKey = typeof(T).Name;
            if (!_queueDictionary.ContainsKey(queueKey))
            {
                _queueDictionary.Add(queueKey, new Queue<T>());
            }


            Queue<T> poolQueue = (Queue<T>)_queueDictionary[queueKey];
            if (poolQueue.Count == 0)
            {
                T newObject = createFunc();
                return newObject;
            }

            T obj = poolQueue.Dequeue();
            activateAction(obj);
            return obj;
        }

        public static void ReleaseObj<T>(T obj, Func<T, object> releaseFunc = null)
        {
            string queueKey = typeof(T).Name;

            if (!_queueDictionary.ContainsKey(queueKey))
            {
                return;
            }
            
            releaseFunc?.Invoke(obj);
            (_queueDictionary[queueKey] as Queue<T>)?.Enqueue(obj);
        }
    }
}