using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestNarration.Model
{
    [System.Serializable]
    public class Episode
    {
        public string episodeId;
        public List<ChapterData> chapters;
    }
}
