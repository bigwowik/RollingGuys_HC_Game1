using CodeBase.Infrastructure.Factory;
using CodeBase.Logic.Player;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class GameLoopState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly Hero _hero;

        public GameLoopState(IGameStateMachine gameStateMachine, Hero hero)
        {
            _gameStateMachine = gameStateMachine;
            _hero = hero;
        }

        public void Enter()
        {
            Debug.Log("Start game loop");
            _hero.CanMove = true;
        }

        public void Exit()
        {
        }
    }
    
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
            Debug.Log("MainMenuState");
        }

        public void Exit()
        {
            _mediator.DisableMainMenu();
        }
    }
}