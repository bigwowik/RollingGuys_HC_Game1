using System;

namespace CodeBase.UI.Windows.EndLevelWindow
{
    public interface IEndLevelWindowModel
    {
        Action MainButtonAction { get; set; }
        Action AdditionalButtonAction { get; set; }
    }
}