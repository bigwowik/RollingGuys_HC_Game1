using System;
using CodeBase.Helpers.Debug;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Inputs;
using CodeBase.Infrastructure.Services.Ads;
using CodeBase.Infrastructure.Services.Progress;
using CodeBase.Logic.Player;
using CodeBase.UI.Factory;
using CodeBase.UI.Hud;
using CodeBase.UI.Windows.EndLevelWindow;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.States
{
    public class EndLevelState : IPayloadState<LevelResult>
    {
        private const float DelayWinLevelWindow = 3.5f;
        private const float DelayFailLevelWindow = 2f;
        
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IUIFactory _uiFactory;
        private readonly IProgressService _progressService;
        private readonly IInputService _inputService;
        private readonly ILevelProgressService _levelProgressService;
        private readonly IRewardService _rewardService;
        private readonly IAdsService _adsService;
        private readonly IGameFactory _gameFactory;

        private EndLevelWindowPresenter _endLevelWindow;

        public EndLevelState(IGameStateMachine gameStateMachine, 
            IUIFactory uiFactory, 
            IProgressService progressService, 
            IInputService inputService,
            ILevelProgressService levelProgressService,
            IRewardService rewardService,
            IAdsService adsService, 
            IGameFactory gameFactory)
        {
            _gameStateMachine = gameStateMachine;
            _uiFactory = uiFactory;
            _progressService = progressService;
            _inputService = inputService;
            _levelProgressService = levelProgressService;
            _rewardService = rewardService;
            _adsService = adsService;
            _gameFactory = gameFactory;
        }

        public void Enter(LevelResult levelResult)
        {
            CreateWinCamera();
            
            var endLevelData = CreateEndLevelData(levelResult);

            ProgressUpdate(endLevelData);
            
            var delayFailLevelWindow = levelResult == LevelResult.WIN ? DelayWinLevelWindow : DelayFailLevelWindow;
            Observable.Timer(TimeSpan.FromSeconds(delayFailLevelWindow))
                .Subscribe(_ =>
                    CreateEndLevelWindow(levelResult, endLevelData));
            //CreateEndLevelWindow(levelResult, endLevelData);
        }

        private void CreateWinCamera()
        {
            _gameFactory.CreatePlayerWinCamera();
        }

        private void CreateEndLevelWindow(LevelResult levelResult, EndLevelData endLevelData)
        {
            _endLevelWindow = _uiFactory.CreateEndLevelWindow(levelResult);
            _endLevelWindow.Start(endLevelData);
            _endLevelWindow.Closed += ReloadGame;
        }

        private EndLevelData CreateEndLevelData(LevelResult levelResult)
        {
            var collectedCoins = _levelProgressService.GetAndResetCollectedCoins();
            var dataLevelEnded = new EndLevelData()
            {
                Result = levelResult,
                CollectedCoins = collectedCoins
            };
            return dataLevelEnded;
        }

        private void ReloadGame()
        {
            //_adsService.ShowAd();
            
            WDebug.Log(WType.Infrastructure, "Reload scene");
            
            _endLevelWindow.Closed -= ReloadGame;
            //reload scene
            var levelName = SceneManager.GetActiveScene().name;
            _gameStateMachine.Enter<LoadLevelState, string>(levelName);
        }

        private void ProgressUpdate(EndLevelData endLevelData)
        {
            _rewardService.GiveClassicReward(endLevelData);
            ProgressUpdate(endLevelData.Result);
            _progressService.Save();
        }

        private void ProgressUpdate(LevelResult payload)
        {
            switch (payload)
            {
                case LevelResult.WIN:
                    WDebug.Log(WType.Infrastructure, "Win");
                    _progressService.CompleteLevel(true);
                    break;
                case LevelResult.FAIL:
                    WDebug.Log(WType.Infrastructure, "Fail");
                    _progressService.CompleteLevel(false);
                    //
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(payload), payload, null);
            }
        }

        public void Exit()
        {
            //_inputService.isBlocked = false;
        }
    }
}