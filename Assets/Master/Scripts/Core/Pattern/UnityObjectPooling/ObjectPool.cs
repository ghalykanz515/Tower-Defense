using System.Collections.Generic;
using UnityEngine;

namespace UnityObjectPooling 
{
    public class ObjectPool<T> where T : MonoBehaviour
    {
        private readonly T prefab;
        private readonly Transform parent;
        private readonly Queue<T> pool = new Queue<T>();
        private readonly HashSet<T> activeObjects = new HashSet<T>();

        public ObjectPool(T prefab, int initialSize, Transform parent = null)
        {
            this.prefab = prefab;
            this.parent = parent;
            ExpandPool(initialSize);
        }

        private void ExpandPool(int count)
        {
            for (int i = 0; i < count; i++)
            {
                T instance = Object.Instantiate(prefab, parent);
                instance.gameObject.SetActive(false);
                pool.Enqueue(instance);
            }
        }

        public void Prewarm(int count)
        {
            ExpandPool(count);
        }

        public T Get()
        {
            while (pool.Count > 0)
            {
                T instance = pool.Dequeue();
                if (instance != null)
                {
                    activeObjects.Add(instance);
                    instance.gameObject.SetActive(true);
                    return instance;
                }
            }

            ExpandPool(1);
            return Get();
        }

        public void ReturnToPool(T instance)
        {
            if (!activeObjects.Contains(instance))
                return;

            activeObjects.Remove(instance);
            instance.gameObject.SetActive(false);
            pool.Enqueue(instance);
        }
    }
}
