using System;
using System.Collections;
using System.Collections.Generic;
using QuestNarration.Model;
using TMPro;
using UnityEngine;

namespace QuestNarration
{
    /// <summary>
    /// Represents a episode
    /// </summary>
    public class UIEpisodeItem : MonoBehaviour
    {
        // Event to be triggered when a chapter is simulated
        public Action<ChapterData, int> OnChapterSimulate;

        // List to hold UI items for chapters
        public List<UIChapterItem> chaptersUI { private set; get; }

        // Dictionary to map chapter data to UI items.
        public Dictionary<ChapterData, UIChapterItem> ChaptersDict;

        [SerializeField] TextMeshProUGUI episodeNameText;

        //container for chapter UI items
        [SerializeField] RectTransform chaptersHolder;

        //current episode data
        Episode episode;

        /// <summary>
        /// Initialize episode UI item with the given episode data and chapter prefab
        /// </summary>
        public void Initialize(Episode episode, UIChapterItem uIChapterItemsPrefab)
        {
            this.episode = episode;
            SetText();
            PopulateChapters(uIChapterItemsPrefab);
        }

        /// <summary>
        /// text for the episode name
        /// </summary>
        private void SetText()
        {
            episodeNameText.text = "EPISODE: " + episode.episodeId;
        }

        /// <summary>
        /// Instantiates and initializ chapter UI items for each chapter in the episode
        /// </summary>
        private void PopulateChapters(UIChapterItem uIChapterItemsPrefab)
        {
            chaptersUI = new();
            ChaptersDict = new();
            for (int i = 0; i < episode.chapters.Count; i++)
            {
                chaptersUI.Add(Instantiate(uIChapterItemsPrefab, chaptersHolder));
                chaptersUI[i].Initialize(episode.chapters[i]);
                chaptersUI[i].OnSimulateAction += ChapterSimulate;
                ChaptersDict.Add(episode.chapters[i], chaptersUI[i]);
            }
        }

        /// <summary>
        /// chapter simulation by triggering the OnChapterSimulate event
        /// </summary>
        private void ChapterSimulate(ChapterData chapterData, int count)
        {
            OnChapterSimulate?.Invoke(chapterData, count);
        }


    }
}
