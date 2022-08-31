using DG.Tweening;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace CodeBase.Logic.Enemy
{
    public class EnemyBallInSkyAnimation : MonoBehaviour
    {
        [SerializeField] private float HorizontalHalfDistance = 2f;
        [SerializeField] private float Height = 5f;
        [SerializeField] private float Time = 1f;
        private Transform _childColliderTransform;
        private void Start()
        {
            _childColliderTransform = transform.GetChild(0);
            
            Sequence s = DOTween.Sequence();

            var center = transform.position;
            var point1 = center + Vector3.right * HorizontalHalfDistance;
            var point2 = center + Vector3.up * Height;
            var point3 = center - Vector3.right * HorizontalHalfDistance;

            _childColliderTransform.transform.position = point1;


            s.Append(_childColliderTransform.DOJump(point3, Height, 1, Time));
            s.Append(_childColliderTransform.DOJump(point1, Height, 1, Time));
            // s.Append(_childColliderTransform.DOMove(point1, Time));
            // s.Append(_childColliderTransform.DOMove(point2, Time));
            // s.Append(_childColliderTransform.DOMove(point3, Time));
            // s.Append(_childColliderTransform.DOMove(point2, Time));
            s.SetLoops(-1, LoopType.Restart);

            _childColliderTransform.OnDestroyAsObservable()
                .Take(1)
                .Subscribe(_ => s.Kill());
        }
    }
}