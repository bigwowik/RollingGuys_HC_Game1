using CodeBase.Helpers.Debug;
using CodeBase.Infrastructure.Inputs;
using CodeBase.Infrastructure.Services.Progress;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class MainMenuState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IMediator _mediator;
        private readonly IProgressService _progressService;
        private readonly IInputService _inputService;

        public MainMenuState(IGameStateMachine gameStateMachine, IMediator mediator, IProgressService progressService, IInputService inputService)
        {
            _gameStateMachine = gameStateMachine;
            _mediator = mediator;
            _progressService = progressService;
            _inputService = inputService;
        }

        public void Enter()
        {
            _inputService.isBlocked = true;
            SetLevelProgressText();
            
        }

        public void Exit()
        {
            _mediator.DisableMainMenuButtons();
        }

        private void SetLevelProgressText()
        {
            var levelText = $"Level {_progressService.ProgressData.LevelProgressData.LevelCurrent}";
            _mediator.SetLevelText(levelText);
        }
    }
}