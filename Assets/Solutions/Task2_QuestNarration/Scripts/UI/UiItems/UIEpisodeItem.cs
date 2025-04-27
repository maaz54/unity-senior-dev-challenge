using System;
using System.Collections;
using System.Collections.Generic;
using QuestNarration.Model;
using TMPro;
using UnityEngine;

namespace QuestNarration
{
    public class UIEpisodeItem : MonoBehaviour
    {
        public Action<ChapterData, int> OnChapterSimulate;
        [SerializeField] TextMeshProUGUI episodeNameText;
        [SerializeField] RectTransform chaptersHolder;
        [SerializeField] List<UIChapterItem> chaptersUI;
        Episode episode;


        public void Initialize(Episode episode, UIChapterItem uIChapterItemsPrefab)
        {
            this.episode = episode;
            PopulateChapters(uIChapterItemsPrefab);
        }

        private void PopulateChapters(UIChapterItem uIChapterItemsPrefab)
        {
            chaptersUI = new();
            for (int i = 0; i < episode.chapters.Count; i++)
            {
                chaptersUI.Add(Instantiate(uIChapterItemsPrefab, chaptersHolder));
                chaptersUI[i].Initialize(episode.chapters[i]);
                chaptersUI[i].OnSimulateAction += ChapterSimulate;
            }
        }

        private void ChapterSimulate(ChapterData chapterData, int count)
        {
            OnChapterSimulate?.Invoke(chapterData, count);
        }


    }
}
