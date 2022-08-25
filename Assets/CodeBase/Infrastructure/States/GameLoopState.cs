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
        private readonly Hero _hero;
        private readonly IMapCreator _mapCreator;
        private readonly LevelProgressHud _levelProgressHud;

        public GameLoopState(IGameStateMachine gameStateMachine, Hero hero, IMapCreator mapCreator, LevelProgressHud levelProgressHud)
        {
            _gameStateMachine = gameStateMachine;
            _hero = hero;
            _mapCreator = mapCreator;
            _levelProgressHud = levelProgressHud;
        }

        public void Enter()
        {
            Debug.Log("Start game loop");
            _hero.CanMove = true;
            _levelProgressHud.StartLevelProgress(_hero,_mapCreator );
            
        }

        public void Exit()
        {
            //_hero.CanMove = false;
        }
    }
}