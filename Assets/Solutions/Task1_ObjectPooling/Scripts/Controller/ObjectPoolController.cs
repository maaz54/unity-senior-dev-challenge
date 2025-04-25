using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace ObjectPooling
{
    public class ObjectPoolController : MonoBehaviour
    {
        [SerializeField] Button spawnButton;
        [SerializeField] MonoBehaviour poolPrefab;
        [SerializeField] float holdTime;
        [SerializeField] int spanwQuantity;
        GenericObjectPool<MonoBehaviour> objectPooler;

        private void Start()
        {
            objectPooler = new GenericObjectPool<MonoBehaviour>(poolPrefab, transform);
            objectPooler.Prewarm(1000);

            spawnButton.onClick.AddListener(OnSpawnButton);
        }

        private void OnSpawnButton()
        {
            SpawnAndRelease(holdTime);
        }


        private void SpawnAndRelease(float holdTime)
        {
            for (int i = 0; i < spanwQuantity; i++)
            {
                MonoBehaviour spawnObj = objectPooler.Get();
                spawnObj.transform.position = UnityEngine.Random.insideUnitCircle * 5f;
                _ = ReleaseAfterDelay(spawnObj, holdTime);
            }

            async Task ReleaseAfterDelay(MonoBehaviour poolObject, float delay)
            {
                await Task.Delay(TimeSpan.FromSeconds(delay));
                objectPooler.Release(poolObject);

            }


        }




    }
}
