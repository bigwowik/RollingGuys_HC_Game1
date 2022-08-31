using System;
using DG.Tweening;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace CodeBase.UI.Animations
{
    public class TutorialFingerAnimation : MonoBehaviour
    {
        [SerializeField] private float AnimationTime;
        [SerializeField] private float HorizontalDistance;

        private void Start()
        {
            transform.localPosition =
                new Vector3(HorizontalDistance, transform.localPosition.y, transform.localPosition.z);
            
            var s = DOTween.Sequence();
            s.Append(transform.DOLocalMoveX(-HorizontalDistance, AnimationTime).SetEase(Ease.InOutSine));
            //s.Append(transform.DOLocalMoveX(HorizontalDistance, AnimationTime));
            s.easeOvershootOrAmplitude = 0.3f;
            
            s.SetLoops(-1, LoopType.Yoyo);

            gameObject.OnDestroyAsObservable()
                .Take(1)
                .Subscribe(_ => s.Kill());
        }
    }
}