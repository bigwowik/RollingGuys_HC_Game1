using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services.Progress;
using CodeBase.UI.Factory;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadDataState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IGameFactory _gameFactory;
        private readonly IProgressService _progressService;
        private readonly IUIFactory _uiFactory;
        private readonly IMediator _mediator;

        public LoadDataState(IGameStateMachine gameStateMachine, IProgressService progressService, IGameFactory gameFactory, IUIFactory uiFactory, IMediator mediator)
        {
            _gameStateMachine = gameStateMachine;
            _gameFactory = gameFactory;
            _progressService = progressService;
            _uiFactory = uiFactory;
            _mediator = mediator;
        }
        public void Enter()
        {
            //load data
            _progressService.Init();
            
            //set start UI
            var data = _progressService.LoadData();
            _mediator.SetCoinsText(data.ResourcesData.Coins.ToString());
            
            
            PrepareFactory();
            
            InitMap();
            InitPlayer();
            InitStartMenu();
            
            
            _gameStateMachine.Enter<MainMenuState>();
        }

        private void PrepareFactory()
        {
            _gameFactory.PrepareFactory();
        }

        private void InitStartMenu()
        {
            _uiFactory.CreateStartMenu();
        }

        private void InitPlayer()
        {
            var player = _gameFactory.CreateHero(Vector3.zero);
            var camera = _gameFactory.CreatePlayerCamera();
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