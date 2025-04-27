using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestNarration.Model
{
    [System.Serializable]
    public class Chapter
    {
        public string chapterId;
        public string completionTrigger;
        public List<Dialogue> dialogues;
    }
}
