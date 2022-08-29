using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace CodeBase.Logic.Friends
{
    public abstract class TriggerInteractiveBase<T> : MonoBehaviour
    {
        private void Start()
        {
            this.OnTriggerEnterAsObservable()
                .Where(c => c.TryGetComponent<T>(out var friend))
                .Take(1)
                .Subscribe(c => OnTriggerAction(c.gameObject));
        }
        protected abstract void OnTriggerAction(GameObject triggerObject);
    }
}