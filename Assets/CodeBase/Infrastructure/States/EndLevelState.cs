using System;
using CodeBase.Helpers.Debug;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Inputs;
using CodeBase.Infrastructure.Services.Progress;
using CodeBase.Logic.Player;
using CodeBase.UI.Factory;
using CodeBase.UI.Hud;
using CodeBase.UI.Windows.EndLevelWindow;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.States
{
    public class EndLevelState : IPayloadState<LevelResult>
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IUIFactory _uiFactory;
        private readonly IProgressService _progressService;
        private readonly IInputService _inputService;
        private readonly ILevelProgressService _levelProgressService;
        private readonly IRewardService _rewardService;

        private EndLevelWindowPresenter _endLevelWindow;

        public EndLevelState(IGameStateMachine gameStateMachine, 
            IUIFactory uiFactory, 
            IProgressService progressService, 
            IInputService inputService,
            ILevelProgressService levelProgressService,
            IRewardService rewardService)
        {
            _gameStateMachine = gameStateMachine;
            _uiFactory = uiFactory;
            _progressService = progressService;
            _inputService = inputService;
            _levelProgressService = levelProgressService;
            _rewardService = rewardService;
        }

        public void Enter(LevelResult levelResult)
        {
            BlockInput();
            
            var endLevelData = CreateEndLevelData(levelResult);
            
            CreateEndLevelWindow(levelResult, endLevelData);
            ProgressUpdate(endLevelData);

        }

        private void BlockInput()
        {
            //_inputService.isBlocked = true;
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