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
        private readonly HeroView _heroView;
        private readonly HeroPresenter _heroPresenter;

        public GameLoopState(IGameStateMachine gameStateMachine, 
            IMapCreator mapCreator, 
            LevelProgressHud levelProgressHud, 
            HeroView heroView,
            HeroPresenter heroPresenter)
        {
            _gameStateMachine = gameStateMachine;
            _mapCreator = mapCreator;
            _levelProgressHud = levelProgressHud;
            _heroView = heroView;
            _heroPresenter = heroPresenter;
        }

        public void Enter()
        {
            Debug.Log("Start game loop");
            _levelProgressHud.StartLevelProgress(_heroView.transform,_mapCreator );
            
        }

        public void Exit()
        {
            //_hero.CanMove = false;
        }
    }
}