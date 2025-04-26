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
            AddButtonLisener();
        }

        private void AddButtonLisener()
        {
            spawnButton.onClick.AddListener(OnSpawnButton);
        }

        private void RemoveButtonLisenter()
        {
            spawnButton.onClick.RemoveAllListeners();
        }

        private void OnSpawnButton()
        {
            RemoveButtonLisenter();
            _ = SpawnAndRelease(holdTime);
        }


        private async Task SpawnAndRelease(float holdTime)
        {

            for (int i = 0; i < spanwQuantity; i++)
            {
                MonoBehaviour spawnObj = objectPooler.Get();
                spawnObj.transform.position = UnityEngine.Random.insideUnitCircle * 5f;
                _ = ReleaseAfterDelay(spawnObj, holdTime);
            }
            await Task.Delay(TimeSpan.FromSeconds(holdTime));
            AddButtonLisener();

            async Task ReleaseAfterDelay(MonoBehaviour poolObject, float delay)
            {
                await Task.Delay(TimeSpan.FromSeconds(delay));
                objectPooler.Release(poolObject);

            }
        }
    }
}
