using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.States;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Buttons
{
    public class LevelLoadButton : ButtonBase
    {
        [SerializeField] private string LoadLevelName;
    
        private IGameStateMachine _gameStateMachine;

        protected override void Construct()
        {
            //_gameStateMachine = AllServices.Container.Single<IGameStateMachine>();
        }
        protected override void OnButtonClickHandler()
        {
            _gameStateMachine.Enter<LoadLevelState,string>(LoadLevelName);
        }
    }
}