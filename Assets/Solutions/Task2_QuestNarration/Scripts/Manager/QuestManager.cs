using System;
using System.Collections;
using System.Collections.Generic;
using QuestNarration.Extensions;
using QuestNarration.Model;
using UnityEngine;

namespace QuestNarration
{
    /// <summary>
    /// Manages the quest system, loading data and checking chapter completion triggers
    /// </summary>
    public class QuestManager : MonoBehaviour
    {
        //Reference of a UI controller
        [SerializeField] UIController uIController;

        //Reference to the UI controller
        EpisodeData episodeData;

        //Invokes when a chapter is completed
        public event Action<ChapterData> OnChapterComplete;

        //Reference to the current chapter being simulated
        ChapterData currentChapter;

        private void Start()
        {
            LoadJsonData();
            SetUI();
            SetupChapterSimulationCounts();
        }


        /// <summary>
        /// Sets up the required simulation counts for each chapter based on the completion trigger.
        /// </summary>
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

        /// <summary>
        /// Initializing the UIs and sets up event listeners.
        /// </summary>
        private void SetUI()
        {
            if (episodeData != null)
            {
                uIController.InitializeUI(episodeData.episodes);
                uIController.OnChapterSimulate += CheckTrigger;
                OnChapterComplete += uIController.OnChapterComplete;
            }
        }

        /// <summary>
        /// Checks the trigger condition after chapter is simulates.
        /// </summary>
        public void CheckTrigger(ChapterData chapterData, int count)
        {
            this.currentChapter = chapterData;
            CheckTrigger(chapterData.completionTrigger, count);
        }

        /// <summary>
        /// checking if the chapter's required simulation count has been met.
        /// </summary>
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

        /// <summary>
        /// Loads data from a json file from Resources folder.
        /// </summary>
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
