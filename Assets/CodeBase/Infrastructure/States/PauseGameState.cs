using CodeBase.Infrastructure.Factory;
using CodeBase.UI.Windows;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class PauseGameState : IState
    {
        private readonly IWindowService _windowService;
        private readonly IGameFactory _gameFactory;

        public PauseGameState(IWindowService windowService, IGameFactory gameFactory)
        {
            _windowService = windowService;
            _gameFactory = gameFactory;
        }

        public void Enter()
        {
            Time.timeScale = 0f;
            
            _windowService.Open(WindowId.Pause);

        }

        public void Exit()
        {
            Time.timeScale = 1f;

        }
    }
}