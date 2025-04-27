using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestNarration.Model
{
    [System.Serializable]
    public class ChapterData
    {
        public string chapterId;
        public string completionTrigger;
        public List<Dialogue> dialogues;
        public bool IsComplete;
        public int RequiredCount;
    }
}
