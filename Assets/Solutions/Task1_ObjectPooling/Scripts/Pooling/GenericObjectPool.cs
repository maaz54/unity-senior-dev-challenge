using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPooling
{
    /// <summary>
    /// A generic object pool to manage pooling and reusing objects of type T
    /// </summary>
    public class GenericObjectPool<T> where T : MonoBehaviour
    {
        //holds the pooled objects
        private Queue<T> pool = new Queue<T>();

        //The prefab of the object to pool
        private T prefab;

        //parent transform for spawned objects
        Transform parent;

        /// <summary>
        /// initializing the pool with a prefab and parent transform
        /// </summary>
        public GenericObjectPool(T prefab, Transform parent)
        {
            this.prefab = prefab;
            this.parent = parent;
        }

        /// <summary>
        /// Prewarms the pool by instantiat a set number of objects to the pool
        /// </summary>
        public void Prewarm(int count)
        {
            for (int i = 0; i < count; i++)
            {
                ExpandPool();
            }
        }

        /// <summary>
        /// Gets an object from the pool. If the pool is empty, it expands
        /// </summary>
        public T Get()
        {
            if (pool.Count == 0)
            {
                ExpandPool();
            }

            T obj = pool.Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }

        /// <summary>
        /// instantiating a new object and adding it to the pool Queue
        /// </summary>
        private void ExpandPool()
        {
            T obj = Object.Instantiate(prefab, parent);
            obj.gameObject.SetActive(false);
            pool.Enqueue(obj);
        }

        /// <summary>
        /// Releases an object back to the pool
        /// </summary>
        public void Release(T obj)
        {
            obj.gameObject.SetActive(false);
            obj.transform.parent = parent;
            pool.Enqueue(obj);
        }
    }
}
