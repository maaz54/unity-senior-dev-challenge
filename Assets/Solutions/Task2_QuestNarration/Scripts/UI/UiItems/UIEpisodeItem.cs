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
        public List<UIChapterItem> chaptersUI { private set; get; }
        public Dictionary<ChapterData, UIChapterItem> ChaptersDict;
        [SerializeField] TextMeshProUGUI episodeNameText;
        [SerializeField] RectTransform chaptersHolder;
        Episode episode;


        public void Initialize(Episode episode, UIChapterItem uIChapterItemsPrefab)
        {
            this.episode = episode;
            SetText();
            PopulateChapters(uIChapterItemsPrefab);
        }

        private void SetText()
        {
            episodeNameText.text = "EPISODE: " + episode.episodeId;
        }

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

        private void ChapterSimulate(ChapterData chapterData, int count)
        {
            OnChapterSimulate?.Invoke(chapterData, count);
        }


    }
}
