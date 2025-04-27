using System.Collections;
using System.Collections.Generic;
using QuestNarration.Model;
using TMPro;
using UnityEngine;

namespace QuestNarration
{
    public class UIChapterItem : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI chapterNameText;
        [SerializeField] TextMeshProUGUI completionTriggerText;

        [SerializeField] ChapterData chapterData;

        public void Initialize(ChapterData chapterData)
        {
            this.chapterData = chapterData;
            SetText();
        
        }

        private void SetText()
        {
            this.chapterNameText.text = chapterData.chapterId;
            this.completionTriggerText.text = chapterData.completionTrigger;
        }


    }
}
