using DG.Tweening;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace CodeBase.Logic.Enemy
{
    public class EnemyHummerAnimation : MonoBehaviour
    {
        [SerializeField] private Vector3 StartRotation;
        [SerializeField] private Vector3 EndRotation;
        [SerializeField] private float Time = 1f;
        [SerializeField] private float TimeDelay = 0.5f;
        [SerializeField] private float TimeBack = 0.5f;
        [SerializeField] private float Overshoot = 1.1f;


        private Transform _childColliderTransform;

        private void Start()
        {
            _childColliderTransform = transform.GetChild(0);

            Sequence s = DOTween.Sequence();

            s.Append(_childColliderTransform.DORotate(EndRotation, Time, RotateMode.Fast).SetEase(Ease.Linear));
            s.Append(_childColliderTransform.DORotate(StartRotation, TimeBack, RotateMode.Fast).SetEase(Ease.OutBack, Overshoot).SetDelay(TimeDelay));
            s.SetLoops(-1, LoopType.Restart);

            _childColliderTransform.OnDestroyAsObservable()
                .Take(1)
                .Subscribe(_ => s.Kill());
            

        }
    }
}