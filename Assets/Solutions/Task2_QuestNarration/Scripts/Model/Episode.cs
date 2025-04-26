using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestNarration.Model
{
    [System.Serializable]
    public class Episode
    {
        public int episodeId { get; set; }
        public List<Chapter> chapters { get; set; }
    }
}
