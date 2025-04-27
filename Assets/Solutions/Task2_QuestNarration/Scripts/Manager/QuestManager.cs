using System;
using System.Collections;
using System.Collections.Generic;
using QuestNarration.Model;
using UnityEngine;

namespace QuestNarration
{
    public class QuestManager : MonoBehaviour
    {
        public List<Episode> Episodes { get; private set; }

        event Action<ChapterData> OnChapterComplete;

        private void Start()
        {
            LoadData();
        }

        private void LoadData()
        {
            TextAsset jsonData = Resources.Load<TextAsset>("Data");
            if (jsonData != null)
            {
                EpisodeData episodeData = JsonUtility.FromJson<EpisodeData>(jsonData.text);
                Episodes = episodeData.episodes;
            }
            else
            {
                Debug.LogError("Quest Data not found!");
            }
        }


    }
}
