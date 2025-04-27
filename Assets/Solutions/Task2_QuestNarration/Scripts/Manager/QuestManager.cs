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
        public event Action<ChapterData> OnChapterComplete;

        ChapterData currentChapter;

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
                    if (!episodeData.episodes[i].chapters[j].IsComplete)
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
                OnChapterComplete += uIController.OnChapterComplete;
            }
        }

        public void CheckTrigger(ChapterData chapterData, int count)
        {
            this.currentChapter = chapterData;
            CheckTrigger(chapterData.completionTrigger, count);
        }

        private void CheckTrigger(string action, int count)
        {
            if (!currentChapter.IsComplete)
            {
                if (count >= currentChapter.RequiredCount)
                {
                    currentChapter.IsComplete = true;
                    OnChapterComplete?.Invoke(currentChapter);
                }
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
