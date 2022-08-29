using CodeBase.Helpers.Debug;
using CodeBase.Infrastructure.Services.Progress;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class MainMenuState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IMediator _mediator;
        private readonly IProgressService _progressService;

        public MainMenuState(IGameStateMachine gameStateMachine, IMediator mediator, IProgressService progressService)
        {
            _gameStateMachine = gameStateMachine;
            _mediator = mediator;
            _progressService = progressService;
        }

        public void Enter()
        {
            WDebug.Log("MainMenuState", WType.GameStates);

            SetLevelProgressText();
        }

        public void Exit()
        {
            _mediator.DisableMainMenu();
        }

        private void SetLevelProgressText()
        {
            var levelText = $"Level {_progressService.ProgressData.LevelCurrent}";
            _mediator.SetLevelText(levelText);
        }
    }
}