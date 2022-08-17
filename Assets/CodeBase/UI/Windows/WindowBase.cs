using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows
{
    public abstract class WindowBase : MonoBehaviour
    {
        public Button CloseButton;

        public void Construct()
        {
        }

        private void Awake() => 
            OnAwake();

        private void Start()
        {
            Initialize();
            SubscribeUpdate();
        }

        private void OnDestroy() => 
            CleanUp();

        protected virtual void OnAwake() => 
            CloseButton.onClick.AddListener( () => Destroy(gameObject));

        protected virtual void Initialize(){}
        protected virtual void SubscribeUpdate(){}
        protected virtual void CleanUp(){}
        
        
        
    }
}