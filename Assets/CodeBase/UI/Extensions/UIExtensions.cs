using TMPro;
using UnityEngine.UI;

namespace CodeBase.UI.Extensions
{
    public static class UIExtensions
    {
        public static void SetTextTMP(this Button button, string text)
        {
            button.GetComponentInChildren<TextMeshProUGUI>().text = text;
        }
    }
}