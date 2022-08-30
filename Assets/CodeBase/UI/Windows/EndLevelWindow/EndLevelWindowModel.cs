using System;
using CodeBase.Infrastructure.Services.Progress;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.EndLevelWindow
{
    public class EndLevelWindowModel : IEndLevelWindowModel
    {
        public Action MainButtonAction { get; set; }
        public Action AdditionalButtonAction { get; set; }

        
    }
}