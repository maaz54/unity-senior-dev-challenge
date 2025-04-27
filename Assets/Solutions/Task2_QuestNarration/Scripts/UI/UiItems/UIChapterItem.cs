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
    /// <summary>
    /// Represents a episode
    /// </summary>
    public class UIChapterItem : MonoBehaviour
    {
        //Button to trigger chapter simulation
        [SerializeField] Button simulateActionButton;

        //Text to display chapter name
        [SerializeField] TextMeshProUGUI chapterNameText;

        //Text to display completion trigger.
        [SerializeField] TextMeshProUGUI completionTriggerText;

        //display when the chapter is complete
        [SerializeField] GameObject chapterCompletePanel;
        [SerializeField] ChapterData chapterData;

        //Counter to track the number of simulations
        [SerializeField] int simulateCount;

        // Event to be triggered when a chapter is simulated
        public Action<ChapterData, int> OnSimulateAction;

        /// <summary>
        /// Initializes the chapter UI item
        /// </summary>
        public void Initialize(ChapterData chapterData)
        {
            this.chapterData = chapterData;
            SetText();
            simulateCount = 0;
            simulateActionButton.onClick.AddListener(OnSimulateButton);
        }


        /// <summary>
        /// Handles the simulate button click
        /// </summary>
        private void OnSimulateButton()
        {
            simulateCount++;
            SetCompletionTriggerText();
            OnSimulateAction?.Invoke(chapterData, simulateCount);
        }


        /// <summary>
        /// setup  text for chapter name and completion trigger
        /// </summary>
        private void SetText()
        {
            this.chapterNameText.text = "CHAPTER: " + chapterData.chapterId;
            this.completionTriggerText.text = chapterData.completionTrigger;
        }

        /// <summary>
        /// Updates the completion trigger text when Chapter Simulates
        /// </summary>
        private void SetCompletionTriggerText()
        {
            string updatedText = Regex.Replace(chapterData.completionTrigger, @"(\d+)", (chapterData.RequiredCount - simulateCount).ToString());
            this.completionTriggerText.text = updatedText;
        }

        /// <summary>
        /// displaying the completion panel.
        /// </summary>
        public void OnChapterComplete()
        {
            simulateActionButton.onClick.RemoveAllListeners();
            simulateActionButton.gameObject.SetActive(false);
            chapterCompletePanel.SetActive(true);
        }


    }
}
