using System;

namespace CodeBase.UI.Windows.EndLevelWindow
{
    public interface IEndLevelWindowModel
    {
        void SetMainButtonAction(Action onMainButtonAction);
        void SetAdditionalButtonAction(Action onAdditionalButtonAction);
    }
}