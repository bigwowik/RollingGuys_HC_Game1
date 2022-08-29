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

        public GameLoopState(IGameStateMachine gameStateMachine, 
            IMapCreator mapCreator, 
            LevelProgressHud levelProgressHud, IGameFactory gameFactory)
        {
            _gameStateMachine = gameStateMachine;
            _mapCreator = mapCreator;
            _levelProgressHud = levelProgressHud;
            _gameFactory = gameFactory;
        }

        public void Enter()
        {
            Debug.Log("Start game loop");
            _levelProgressHud.StartLevelProgress(_mapCreator);
            
        }

        public void Exit()
        {
            //_hero.CanMove = false;
        }
    }
}