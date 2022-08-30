using System;
using CodeBase.Helpers.Debug;
using CodeBase.Infrastructure.States;
using UniRx;
using UnityEngine;

namespace CodeBase.UI.Windows.EndLevelWindow
{
    public class EndLevelWindowPresenter 
    {
        private readonly EndLevelWindowView _view;
        private readonly EndLevelWindowModel _model;
        private readonly IRewardService _rewardService;
        public event Action Closed;

        public EndLevelWindowPresenter(EndLevelWindowView view, EndLevelWindowModel model, IRewardService rewardService)
        {
            _view = view;
            _model = model;
            _rewardService = rewardService;
        }

        private void CloseWindow()
        {
            GameObject.Destroy(_view.gameObject);
            Closed?.Invoke();
        }

        public void Start(EndLevelData endLevelData)
        {
            SetupView(endLevelData);
            
            
            _view.MainButton
                .OnClickAsObservable()
                .Take(1)
                .Subscribe(z =>
                {
                    _rewardService.GiveAdsReward(endLevelData);
                    WDebug.Log("Main Button Action", WType.UI);
                    CloseWindow();
                });
            
            _view.AdditionalButton
                .OnClickAsObservable()
                .Take(1)
                .Subscribe(z =>
                {
                    //_rewardService.GiveClassicReward(endLevelData);
                    WDebug.Log("Additional Button Action", WType.UI);
                    CloseWindow();
                    
                });
        }

        private void SetupView(EndLevelData endLevelData)
        {
            switch (endLevelData.EndLevelType)
            {
                case EndLevelType.WIN:
                    _view.SetResultText("WIN");
                    _view.SetEarnValue(endLevelData.CollectedCoins);
                    _view.SetMainButtonText("x3 by ADS");
                    _view.SetAdditionalButton("No thanks");
                    break;
                case EndLevelType.FAIL:
                    _view.SetResultText("FAIL");
                    _view.SetEarnValue(endLevelData.CollectedCoins);
                    _view.SetMainButtonText("x2 by ADS");
                    _view.SetAdditionalButton("No thanks");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(endLevelData.EndLevelType), endLevelData.EndLevelType, null);
            }
        }
    }
}