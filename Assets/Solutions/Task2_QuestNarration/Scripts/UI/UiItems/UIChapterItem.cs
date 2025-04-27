using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using JetBrains.Annotations;
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
        [SerializeField] GameObject chapterCompletePanel;
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
            simulateCount++;
            SetCompletionTriggerText();
            OnSimulateAction?.Invoke(chapterData, simulateCount);
        }

        private void SetText()
        {
            this.chapterNameText.text = chapterData.chapterId;
            this.completionTriggerText.text = chapterData.completionTrigger;
        }

        private void SetCompletionTriggerText()
        {
            string updatedText = Regex.Replace(chapterData.completionTrigger, @"(\d+)",(chapterData.RequiredCount - simulateCount).ToString());
            this.completionTriggerText.text = updatedText;
        }

        public void OnChapterComplete()
        {
            simulateActionButton.onClick.RemoveAllListeners();
            simulateActionButton.gameObject.SetActive(false);
            chapterCompletePanel.SetActive(true);
        }


    }
}
