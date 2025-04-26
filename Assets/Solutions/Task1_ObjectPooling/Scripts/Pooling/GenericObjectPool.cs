using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPooling
{
    public class GenericObjectPool<T> where T : MonoBehaviour
    {
        private Queue<T> pool = new Queue<T>();
        private T prefab;
        Transform parent;

        public GenericObjectPool(T prefab, Transform parent)
        {
            this.prefab = prefab;
            this.parent = parent;
        }

        public void Prewarm(int count)
        {
            for (int i = 0; i < count; i++)
            {
                ExpandPool();
            }
        }

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
        private void ExpandPool()
        {
            T obj = Object.Instantiate(prefab, parent);
            obj.gameObject.SetActive(false);
            pool.Enqueue(obj);
        }

        public void Release(T obj)
        {
            obj.gameObject.SetActive(false);
            obj.transform.parent = parent;
            pool.Enqueue(obj);
        }
    }
}
