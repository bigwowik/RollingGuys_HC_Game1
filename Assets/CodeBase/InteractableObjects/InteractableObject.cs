using UnityEngine;

namespace CodeBase.InteractableObjects
{
    public abstract class InteractableObject : MonoBehaviour
    {
        public InteractableType InteractableType;
        public abstract void OnInteract();
    }

    public enum InteractableType
    {
        TALK,
        USE
    }
}