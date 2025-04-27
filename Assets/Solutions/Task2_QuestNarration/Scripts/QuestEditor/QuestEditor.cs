using System.Collections;
using System.Collections.Generic;
using QuestNarration.Model;
using UnityEngine;

namespace QuestNarration
{
    public class QuestEditor : MonoBehaviour
    {
        public List<Episode> Episodes;
        string path;

        private void Start()
        {
            LoadJsonData();
        }

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
