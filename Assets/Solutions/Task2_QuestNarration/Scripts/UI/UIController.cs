using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using QuestNarration.Model;
using UnityEngine;

namespace QuestNarration
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] UIChapterItem uIChapterItemPrefab;
        [SerializeField] UIEpisodeItem uIEpisodeItemPrefab;
        [SerializeField] RectTransform episodeHolder;
        [SerializeField] List<UIEpisodeItem> uIEpisodeItems;

        public Action<ChapterData, int> OnChapterSimulate;


        public void InitializeUI(List<Episode> episodes)
        {
            uIEpisodeItems = new();
            for (int i = 0; i < episodes.Count; i++)
            {
                uIEpisodeItems.Add(Instantiate(uIEpisodeItemPrefab, episodeHolder));
                uIEpisodeItems[i].Initialize(episodes[i], uIChapterItemPrefab);
                uIEpisodeItems[i].OnChapterSimulate += OnChapterSimulate;

            }
        }

        private void ChapterSimulate(ChapterData chapterData, int count)
        {
            OnChapterSimulate?.Invoke(chapterData, count);
        }

    }
}
