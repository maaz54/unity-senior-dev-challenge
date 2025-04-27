using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using QuestNarration.Model;
using UnityEngine;

namespace QuestNarration
{
    /// <summary>
    /// Manages the UI components related to Episodes and Chapters
    /// </summary>
    public class UIController : MonoBehaviour
    {
        // Prefab for the Chapter UI item.
        [SerializeField] UIChapterItem uIChapterItemPrefab;

        // Prefab for the Episode UI item.
        [SerializeField] UIEpisodeItem uIEpisodeItemPrefab;

        //holds the episode UI items
        [SerializeField] RectTransform episodeHolder;

        //List of instantiated Episode UI items.
        [SerializeField] List<UIEpisodeItem> uIEpisodeItems;

        //Dictionary to map ChapterData to their corresponding UI items
        Dictionary<ChapterData, UIChapterItem> chaptersDict;

        //// Event triggered when a chapter is simulated
        public Action<ChapterData, int> OnChapterSimulate;


        /// <summary>
        /// Initializes the UI with the list of episodes
        /// </summary>
        public void InitializeUI(List<Episode> episodes)
        {
            uIEpisodeItems = new();
            chaptersDict = new Dictionary<ChapterData, UIChapterItem>();
            for (int i = 0; i < episodes.Count; i++)
            {
                uIEpisodeItems.Add(Instantiate(uIEpisodeItemPrefab, episodeHolder));
                uIEpisodeItems[i].Initialize(episodes[i], uIChapterItemPrefab);
                uIEpisodeItems[i].OnChapterSimulate += ChapterSimulate;
                foreach (var key in uIEpisodeItems[i].ChaptersDict)
                {
                    chaptersDict.Add(key.Key, key.Value);
                }
            }
        }

        /// <summary>
        /// Marks a chapter as complete and updates the UI.
        /// </summary>
        public void OnChapterComplete(ChapterData chapterData)
        {
            if (chaptersDict.TryGetValue(chapterData, out UIChapterItem chapterItem))
            {
                chapterItem.OnChapterComplete();
            }
        }
        
        /// <summary>
        /// Invokes the OnChapterSimulate event when a chapter simulation is triggered.
        /// </summary>
        private void ChapterSimulate(ChapterData chapterData, int count)
        {
            OnChapterSimulate?.Invoke(chapterData, count);
        }

    }
}
