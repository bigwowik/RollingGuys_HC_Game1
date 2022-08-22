using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services.Progress;
using CodeBase.UI.Factory;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadDataState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IGameFactory _gameFactory;
        private readonly IProgressService _progressService;
        private readonly IUIFactory _uiFactory;

        public LoadDataState(IGameStateMachine gameStateMachine, IProgressService progressService, IGameFactory gameFactory, IUIFactory uiFactory)
        {
            _gameStateMachine = gameStateMachine;
            _gameFactory = gameFactory;
            _progressService = progressService;
            _uiFactory = uiFactory;
        }
        public void Enter()
        {
            //load data
            var data = _progressService.LoadData();
            
            InitMap();
            InitPlayer();
            InitStartMenu();
            
            
            _gameStateMachine.Enter<GameLoopState>();
        }

        private void InitStartMenu()
        {
            _uiFactory.CreateStartMenu();
        }

        private void InitPlayer()
        {
            var player = _gameFactory.CreateHero(Vector3.zero);
        }

        private void InitMap()
        {
            var mapCreator = _gameFactory.CreateMapCreator();
            mapCreator.CreateMap();
        }

        public void Exit()
        {
            //
        }
    }
}