using System.Collections;
using System.Collections.Generic;
using QuestNarration.Model;
using UnityEngine;

namespace QuestNarration
{
    /// <summary>
    /// Handles loading and saving data from JSON file.
    /// </summary>
    public class QuestEditor : MonoBehaviour
    {
        public List<Episode> Episodes;

        private void Start()
        {
            LoadJsonData();
        }


        /// <summary>
        /// Loads data from a json file from Resources folder.
        /// </summary>
        private void LoadJsonData()
        {
            TextAsset jsonData = Resources.Load<TextAsset>("Data");
            if (jsonData != null)
            {
                Episodes = JsonUtility.FromJson<EpisodeData>(jsonData.text).episodes;
            }
            else
            {
                Debug.LogError("Quest Data not found!");
            }
        }

        /// <summary>
        /// Saves the current list of episodes to a JSON file.
        /// This method can be called through the Unity editor context menu.
        /// </summary>
        [ContextMenu("SaveEpisodeData")]
        public void SaveEpisodeData()
        {
            EpisodeData episodeData = new()
            {
                episodes = Episodes
            };

            string json = JsonUtility.ToJson(episodeData, true);
            string path = Application.dataPath + "/Solutions/Task2_QuestNarration/Resources/Data.json";
            System.IO.File.WriteAllText(path, json);

        }
    }
}
