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
        Dictionary<ChapterData, UIChapterItem> chaptersDict;

        public Action<ChapterData, int> OnChapterSimulate;


        public void InitializeUI(List<Episode> episodes)
        {
            uIEpisodeItems = new();
            chaptersDict = new Dictionary<ChapterData, UIChapterItem>();
            for (int i = 0; i < episodes.Count; i++)
            {
                uIEpisodeItems.Add(Instantiate(uIEpisodeItemPrefab, episodeHolder));
                uIEpisodeItems[i].Initialize(episodes[i], uIChapterItemPrefab);
                uIEpisodeItems[i].OnChapterSimulate += ChapterSimulate;
                foreach (var kvp in uIEpisodeItems[i].ChaptersDict)
                {
                    chaptersDict.Add(kvp.Key, kvp.Value);
                }
            }
        }

        public void OnChapterComplete(ChapterData chapterData)
        {
            if(chaptersDict.TryGetValue(chapterData, out UIChapterItem chapterItem))
            {
                chapterItem.OnChapterComplete();
            }
        }

        private void ChapterSimulate(ChapterData chapterData, int count)
        {
            OnChapterSimulate?.Invoke(chapterData, count);
        }

    }
}
