using UnityEngine;

namespace CodeBase.UI.Buttons
{
    public class ExitButton : ButtonBase
    {
        protected override void OnButtonClickHandler()
        {
            Application.Quit();
        }
    }
}
