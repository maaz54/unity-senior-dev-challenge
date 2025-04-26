using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestNarration.Model
{
    [System.Serializable]
    public class Chapter
    {
        public string chapterId { get; set; }
        public string completionTrigger { get; set; }
        public List<Dialogue> dialogues { get; set; }
    }
}
