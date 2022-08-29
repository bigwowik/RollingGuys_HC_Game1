using System;
using CodeBase.Helpers.Debug;
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

        private EndLevelWindowPresenter _endLevelWindow;

        public EndLevelState(IGameStateMachine gameStateMachine, IUIFactory uiFactory, IProgressService progressService, IInputService inputService)
        {
            _gameStateMachine = gameStateMachine;
            _uiFactory = uiFactory;
            _progressService = progressService;
            _inputService = inputService;
        }

        public void Enter(EndLevelType endLevelType)
        {
            ProgressUpdate(endLevelType);



            _inputService.isBlocked = true;
            //_hero.CanMove = false;
            
            _endLevelWindow = _uiFactory.CreateEndLevelWindow(endLevelType);
            _endLevelWindow.Start(endLevelType);
            _endLevelWindow.Closed += ReloadGame;


        }

        private void ReloadGame()
        {
            _endLevelWindow.Closed -= ReloadGame;
            
            WDebug.Log(WType.Infrastructure, "Reload scene");

            var levelName = SceneManager.GetActiveScene().name;
            _gameStateMachine.Enter<LoadLevelState, string>(levelName);
            
            
            //reload scene
        }

        private void ProgressUpdate(EndLevelType payload)
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