using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Buttons
{
    public abstract class ButtonBase : MonoBehaviour
    {
        
        
        protected Button _button;
        private void Start()
        {
            Construct();
            
            _button = GetComponent<Button>();
            
            Subscribe();
        }
        private void OnDestroy()
        {
            Unsubscribe();
        }

        protected abstract void OnButtonClickHandler();

        private void Unsubscribe()
        {
            _button.onClick.RemoveListener(OnButtonClickHandler);
        }

        private void Subscribe()
        {
            _button.onClick.AddListener(OnButtonClickHandler);
            
        }
        protected virtual void Construct()
        {
        }
    }
}