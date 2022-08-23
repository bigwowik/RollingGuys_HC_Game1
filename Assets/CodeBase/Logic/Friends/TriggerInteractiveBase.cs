using UnityEngine;

namespace CodeBase.Logic.Friends
{
    public abstract class TriggerInteractiveBase<T> : MonoBehaviour
    {
        private bool _wasActivated = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<T>(out var component))
            {
                if (_wasActivated) return;
                _wasActivated = true;
                
                OnTriggerAction(other.gameObject);
            }
        }
        
        protected abstract void OnTriggerAction(GameObject triggerObject);
    }
}