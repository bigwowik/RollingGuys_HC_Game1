using CodeBase.Helpers.Debug;
using CodeBase.Infrastructure.Factory;
using CodeBase.Logic.Map;
using CodeBase.Logic.Player;
using CodeBase.UI.Hud;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class GameLoopState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IMapCreator _mapCreator;
        private readonly LevelProgressHud _levelProgressHud;
        private readonly IGameFactory _gameFactory;
        private readonly ILevelProgressService _levelProgressService;

        public GameLoopState(IGameStateMachine gameStateMachine, 
            IMapCreator mapCreator, 
            LevelProgressHud levelProgressHud, 
            IGameFactory gameFactory,
            ILevelProgressService levelProgressService)
        {
            _gameStateMachine = gameStateMachine;
            _mapCreator = mapCreator;
            _levelProgressHud = levelProgressHud;
            _gameFactory = gameFactory;
            _levelProgressService = levelProgressService;
        }

        public void Enter()
        {
            WDebug.Log("Start game loop", WType.GameStates);
            _levelProgressHud.StartLevelProgress(_mapCreator);
            _levelProgressService.StartLevel();
            
        }

        public void Exit()
        {
            //_hero.CanMove = false;
        }
    }
}