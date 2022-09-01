using System;
using CodeBase.Helpers.Debug;
using CodeBase.Infrastructure.Services.Ads;
using CodeBase.Infrastructure.States;
using UniRx;
using UnityEngine;
using UnityEngine.Advertisements;

namespace CodeBase.UI.Windows.EndLevelWindow
{
    public class EndLevelWindowPresenter 
    {
        //services
        private readonly EndLevelWindowView _view;
        private readonly EndLevelWindowModel _model;
        private readonly IRewardService _rewardService;
        private readonly IAdsRewardedService _adsRewardedService;
        
        //variables
        private EndLevelData _endLevelData;
        public event Action Closed;

        public EndLevelWindowPresenter(EndLevelWindowView view, EndLevelWindowModel model, IRewardService rewardService, IAdsRewardedService adsRewardedService)
        {
            _view = view;
            _model = model;
            _rewardService = rewardService;
            _adsRewardedService = adsRewardedService;
        }

        public void Start(EndLevelData endLevelData)
        {
            _endLevelData = endLevelData;
            
            SetupView(_endLevelData);

            SubscribeOnAdsRewardService();
            
            SubscribeOnMainButton();
            SubscribeOnAdditionalButton();
        }

        private void CloseWindow()
        {
            UnSubscribeOnAdsRewardService();

            GameObject.Destroy(_view.gameObject);
            Closed?.Invoke();
        }

        private void SubscribeOnAdditionalButton()
        {
            _view.AdditionalButton
                .OnClickAsObservable()
                .Take(1)
                .Subscribe(z =>
                {
                    WDebug.Log("Additional Button Action", WType.UI);
                    CloseWindow();
                });
        }

        private void SubscribeOnMainButton()
        {
            _view.MainButton
                .OnClickAsObservable()
                .Take(1)
                .Subscribe(z =>
                {
                    WDebug.Log("Main Button Action", WType.UI);
                    _adsRewardedService.ShowAd();
                    
                });
        }

        private void SubscribeOnAdsRewardService()
        {
            _adsRewardedService.OnAdsLoaded += EnableAdsButton;
            _adsRewardedService.OnAdsShowCompleted += AdsCompleted;
            
            if(_adsRewardedService.IsAdsLoaded)
                EnableAdsButton();
        }

        private void AdsCompleted(ShowResult showResult)
        {
            switch (showResult)
            {
                case ShowResult.Failed:
                    break;
                case ShowResult.Skipped:
                    break;
                case ShowResult.Finished:
                    _rewardService.GiveAdsReward(_endLevelData);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(showResult), showResult, null);
            }
            CloseWindow();
        }

        private void UnSubscribeOnAdsRewardService()
        {
            _adsRewardedService.OnAdsLoaded -= EnableAdsButton;
            _adsRewardedService.OnAdsShowCompleted -= AdsCompleted;
        }

        private void EnableAdsButton()
        {
            _view.MainButton.interactable = true;
        }

        private void SetupView(EndLevelData endLevelData)
        {
            _view.MainButton.interactable = false;
            
            switch (endLevelData.Result)
            {
                case LevelResult.WIN:
                    _view.SetResultText("WIN");
                    _view.SetEarnValue(endLevelData.CollectedCoins);
                    _view.SetMainButtonText("x3 by ADS");
                    _view.SetAdditionalButton("No thanks");
                    break;
                case LevelResult.FAIL:
                    _view.SetResultText("FAIL");
                    _view.SetEarnValue(endLevelData.CollectedCoins);
                    _view.SetMainButtonText("x2 by ADS");
                    _view.SetAdditionalButton("No thanks");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(endLevelData.Result), endLevelData.Result, null);
            }
        }
    }
}