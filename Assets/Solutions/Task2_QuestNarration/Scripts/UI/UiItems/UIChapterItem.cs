using System;
using System.Collections;
using System.Collections.Generic;
using QuestNarration.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace QuestNarration
{
    public class UIChapterItem : MonoBehaviour
    {
        [SerializeField] Button simulateActionButton;
        [SerializeField] TextMeshProUGUI chapterNameText;
        [SerializeField] TextMeshProUGUI completionTriggerText;
        [SerializeField] ChapterData chapterData;
        [SerializeField] int simulateCount;
        public Action<ChapterData, int> OnSimulateAction;

        public void Initialize(ChapterData chapterData)
        {
            this.chapterData = chapterData;
            SetText();
            simulateCount = 0;
            simulateActionButton.onClick.AddListener(OnSimulateButton);
        }

        private void OnSimulateButton()
        {
            // simulateActionButton.onClick.RemoveAllListeners();
            simulateCount++;
            OnSimulateAction?.Invoke(chapterData, simulateCount);
        }

        private void SetText()
        {
            this.chapterNameText.text = chapterData.chapterId;
            this.completionTriggerText.text = chapterData.completionTrigger;
        }


    }
}
