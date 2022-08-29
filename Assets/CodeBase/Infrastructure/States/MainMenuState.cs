using CodeBase.Helpers.Debug;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class MainMenuState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IMediator _mediator;

        public MainMenuState(IGameStateMachine gameStateMachine, IMediator mediator)
        {
            _gameStateMachine = gameStateMachine;
            _mediator = mediator;
        }

        public void Enter()
        {
            WDebug.Log("MainMenuState", WType.GameStates);
        }

        public void Exit()
        {
            _mediator.DisableMainMenu();
        }
    }
}