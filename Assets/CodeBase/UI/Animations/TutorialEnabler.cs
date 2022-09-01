using System;
using CodeBase.Helpers.Debug;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Inputs;
using CodeBase.Infrastructure.Services;
using DG.Tweening;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Zenject;

namespace CodeBase.UI.Animations
{
    public class TutorialEnabler : MonoBehaviour
    {
        [SerializeField] private float TimeToShowTutorialAgain = 2f;
        [SerializeField] private float FadeDuration = 0.5f;

        //components
        private CanvasGroup _canvasGroup;

        //services
        private IInputService _inputService;

        //variables
        private bool _isTutorialActive;

        [Inject]
        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void Start()
        {
            _isTutorialActive = true;
            _canvasGroup = GetComponentInChildren<CanvasGroup>();
     
            SubscribeToDisable();

            
        }

        // private void Update()
        // {
        //     if (_isTutorialActive)
        //         if(_inputService.isActive)
        //             TryToDisable();
        //     else if 
        //             (!_inputService.isActive)
        //             TryToEnable();
        // }
        //
        // private void TryToEnable()
        // {
        //     throw new NotImplementedException();
        // }

        private void TryToDisable()
        {
            throw new NotImplementedException();
        }

        private void SubscribeToDisable()
        {
            IDisposable dis = gameObject
                .UpdateAsObservable()
                .Where(_ => _isTutorialActive)
                .Where(_ => _inputService.isActive)
                .Take(1)
                .Subscribe(_ => DisableUI());
            
        }

        private void SubscribeToEnable()
        {
            gameObject
                .UpdateAsObservable()
                .Where(_ => !_isTutorialActive)
                .Where(_ => !_inputService.isActive)
                .Delay(System.TimeSpan.FromSeconds(TimeToShowTutorialAgain))
                .Where(_ => !_isTutorialActive)
                .Take(1)
                .Subscribe(_ => EnableUI());
        }

        private void DisableUI()
        {
            //if (!_isTutorialActive) return;
            
            _isTutorialActive = false;

            _canvasGroup.DOFade(0, FadeDuration).OnComplete(SubscribeToEnable);
        }

        private void EnableUI()
        {
            //if (_isTutorialActive) return;
            
            _isTutorialActive = true;
            _canvasGroup.DOFade(1, FadeDuration).OnComplete(SubscribeToDisable);
        }
    }
}