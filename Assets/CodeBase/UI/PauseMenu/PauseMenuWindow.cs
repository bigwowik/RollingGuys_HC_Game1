using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.States;
using CodeBase.UI.Windows;
using UnityEngine.UI;

namespace CodeBase.UI.PauseMenu
{
    public class PauseMenuWindow : WindowBase
    {
        //public Button ContinueButton;
        public Button MenuButton;
        private IGameStateMachine _gameStateMachine;


        // public void Construct()
        // {
        //     _gameStateMachine = AllServices.Container.Single<IGameStateMachine>();
        // }

        protected override void Initialize()
        {
            Construct();
        }

        protected override void SubscribeUpdate()
        {
            //ContinueButton.onClick.AddListener(ContinueGame);
            MenuButton.onClick.AddListener(GoToMenu);
        }

        protected override void CleanUp()
        {
            ContinueGame();
            //ContinueButton.onClick.RemoveListener(ContinueGame);
            //MenuButton.onClick.RemoveListener(GoToMenu);
        }

        private void ContinueGame()
        {
            _gameStateMachine.Enter<GameLoopState>();
            Destroy(gameObject);
        }

        private void GoToMenu()
        {
            _gameStateMachine.Enter<LoadLevelState, string>("StartMenu");
        }
    }
}