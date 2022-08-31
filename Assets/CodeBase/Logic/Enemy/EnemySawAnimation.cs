using System;
using DG.Tweening;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace CodeBase.Logic.Enemy
{
    public class EnemySawAnimation : MonoBehaviour
    {
        [SerializeField] private Vector3 StartRotation;
        [SerializeField] private Vector3 EndRotation;
        [SerializeField] private float Time = 1f;
        [SerializeField] private float Overshoot = 1.1f;


        private Transform _childColliderTransform;

        private void Start()
        {
            _childColliderTransform = transform.GetChild(0);

            Sequence s = DOTween.Sequence();

            s.Append(_childColliderTransform.DORotate(EndRotation, Time, RotateMode.Fast).SetEase(Ease.OutBack, Overshoot));
            s.Append(_childColliderTransform.DORotate(StartRotation, Time, RotateMode.Fast).SetEase(Ease.OutBack, Overshoot));
            s.SetLoops(-1, LoopType.Restart);

            _childColliderTransform.OnDestroyAsObservable()
                .Take(1)
                .Subscribe(_ => s.Kill());
            

        }
    }
}