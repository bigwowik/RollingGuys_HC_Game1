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
    public class EndLevelState : IPayloadState<EndLevelType>
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

        public void Enter(EndLevelType endLevelType)
        {

            var endLevelData = CreateEndLevelData(endLevelType);
            _rewardService.GiveClassicReward(endLevelData);

            _inputService.isBlocked = true;
            
            _endLevelWindow = _uiFactory.CreateEndLevelWindow(endLevelType);
            _endLevelWindow.Start(endLevelData);
            _endLevelWindow.Closed += ReloadGame;
            
            
            ProgressUpdateAndSave(endLevelType);

        }

        private EndLevelData CreateEndLevelData(EndLevelType endLevelType)
        {
            var collectedCoins = _levelProgressService.GetAndResetCollectedCoins();
            var dataLevelEnded = new EndLevelData()
            {
                EndLevelType = endLevelType,
                CollectedCoins = collectedCoins
            };
            return dataLevelEnded;
        }

        private void ReloadGame()
        {
            _endLevelWindow.Closed -= ReloadGame;
            
            WDebug.Log(WType.Infrastructure, "Reload scene");

            var levelName = SceneManager.GetActiveScene().name;
            _gameStateMachine.Enter<LoadLevelState, string>(levelName);
            
            
            //reload scene
        }

        private void ProgressUpdateAndSave(EndLevelType payload)
        {
            switch (payload)
            {
                case EndLevelType.WIN:
                    WDebug.Log(WType.Infrastructure, "Win");
                    _progressService.CompleteLevel(true);
                    break;
                case EndLevelType.FAIL:
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
            _inputService.isBlocked = false;
        }
    }
}