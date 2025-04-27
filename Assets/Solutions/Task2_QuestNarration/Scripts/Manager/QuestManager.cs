using System;
using System.Collections;
using System.Collections.Generic;
using QuestNarration.Extensions;
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
            SetUI();
            SetupChapterSimulationCounts();
        }

        private void SetupChapterSimulationCounts()
        {
            for (int i = 0; i < episodeData.episodes.Count; i++)
            {
                for (int j = 0; j < episodeData.episodes[i].chapters.Count; j++)
                {
                    if(!episodeData.episodes[i].chapters[j].IsComplete)
                    {
                        episodeData.episodes[i].chapters[j].RequiredCount = episodeData.episodes[i].chapters[j].completionTrigger.ParseNumber(); 
                    }
                }
            }
        }

        private void SetUI()
        {
            if (episodeData != null)
            {
                uIController.InitializeUI(episodeData.episodes);
                uIController.OnChapterSimulate += CheckTrigger;
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

        public void CheckTrigger(ChapterData chapterData, int count)
        {
            CheckTrigger(chapterData.completionTrigger, count);
        }

        private void CheckTrigger(string action, int count)
        {

        }
    }
}
