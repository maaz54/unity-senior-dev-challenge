using System;
using System.Collections;
using System.Collections.Generic;
using QuestNarration.Model;
using UnityEngine;

namespace QuestNarration
{
    public class QuestManager : MonoBehaviour
    {
        [SerializeField] UIController uIController;
        EpisodeData episodeData;
        event Action<ChapterData> OnChapterComplete;

        private void Start()
        {
            LoadJsonData();

            if (episodeData != null)
            {
                uIController.InitializeUI(episodeData.episodes);
            }
        }

        private void LoadJsonData()
        {
            TextAsset jsonData = Resources.Load<TextAsset>("Data");
            if (jsonData != null)
            {
                episodeData = JsonUtility.FromJson<EpisodeData>(jsonData.text);
            }
            else
            {
                Debug.LogError("Quest Data not found!");
            }
        }
    }
}
