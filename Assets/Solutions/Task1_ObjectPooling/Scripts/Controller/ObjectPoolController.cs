using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace ObjectPooling
{
    /// <summary>
    /// responsible for object pooling, handles the spawning and releasing of objects, and interacts with a UI button.
    /// </summary>
    public class ObjectPoolController : MonoBehaviour
    {
        // Reference to the UI button to trigger object spawning.
        [SerializeField] Button spawnButton;

        // The object to be pooled.
        [SerializeField] MonoBehaviour poolPrefab;

        // Time in seconds for which the objects
        [SerializeField] float holdTime;

        // Number of objects to spawn at once.
        [SerializeField] int spawnQuantity;

        GenericObjectPool<MonoBehaviour> objectPooler;

        private void Start()
        {
            objectPooler = new GenericObjectPool<MonoBehaviour>(poolPrefab, transform);
            objectPooler.Prewarm(1000);
            AddButtonLisener();
        }

        /// <summary>
        /// Adds the listener to the spawn button click event.
        /// </summary>
        private void AddButtonLisener()
        {
            spawnButton.onClick.AddListener(OnSpawnButton);
        }

        /// <summary>
        /// Removes all listeners from the spawn button, ensuring no duplicate event triggers.
        /// </summary>
        private void RemoveButtonLisenter()
        {
            spawnButton.onClick.RemoveAllListeners();
        }

        /// <summary>
        /// Handles the spawn button click event.
        /// It removes the button listener, starts the spawning and releasing process, and re-adds the listener after completion.
        /// </summary>
        private void OnSpawnButton()
        {
            RemoveButtonLisenter();
            _ = SpawnAndRelease(holdTime);
        }

        /// <summary>
        /// Spawns a set quantity of objects, positions them randomly within a circle, and releases them after a specified delay.
        /// </summary>
        private async Task SpawnAndRelease(float holdTime)
        {

            for (int i = 0; i < spawnQuantity; i++)
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
