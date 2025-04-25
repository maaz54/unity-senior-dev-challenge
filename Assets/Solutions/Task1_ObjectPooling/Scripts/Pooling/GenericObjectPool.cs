using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPooling
{
    public class GenericObjectPool<T> : MonoBehaviour where T : Component
    {
        private Queue<T> pool = new Queue<T>();
        private T prefab;
        private Transform parent;

        public void Prewarm(int count)
        {
            for (int i = 0; i < count; i++)
            {
                T obj = Instantiate(prefab, parent);
                obj.gameObject.SetActive(false);
                pool.Enqueue(obj);
            }
        }

        public T Get()
        {
            T obj = pool.Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }
        public void Release(T obj)
        {

        }
    }
}
