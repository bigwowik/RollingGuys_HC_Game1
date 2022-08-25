using System;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.EndLevelWindow
{
    public class EndLevelWindowModel : IEndLevelWindowModel
    {
        public Action MainButtonAction;
        public Action AdditionalButtonAction;
        
        
        public void SetMainButtonAction(Action onMainButtonAction)
        {
            throw new NotImplementedException();
        }

        public void SetAdditionalButtonAction(Action onAdditionalButtonAction)
        {
            throw new NotImplementedException();
        }
    }
}