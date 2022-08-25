using System;
using CodeBase.UI.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.EndLevelWindow
{
    public class EndLevelWindowView : MonoBehaviour
    {
        public Button MainButton;
        public Button AdditionalButton;

        public TextMeshProUGUI ResultText;
        public TextMeshProUGUI EarnValueText;
        
        

        
        public void SetEarnValue(int value)
        {
            EarnValueText.text = value.ToString();
        }

        public void SetResultText(string resultText)
        {
            ResultText.text = resultText;
        }

        public void SetMainButtonText(string text)
        {
            MainButton.SetTextTMP(text);
        }

        public void SetAdditionalButton(string text)
        {
            AdditionalButton.SetTextTMP(text);
        }
    }
}