using Cinemachine;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.States;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Progress
{
    public class LevelProgressService : ILevelProgressService
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IProgressService _progressService;
        public Transform ActivePlayer { get; set; }
        public CinemachineVirtualCamera PlayerCamera { get; set; }
        

        public LevelProgressService(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void EndLevelTriggerAction(EndLevelType endLevelType)
        {
            _gameStateMachine.Enter<EndLevelState, EndLevelType>(endLevelType);
            
        }

        public void ReloadLevelWithFail()
        {
            _gameStateMachine.Enter<EndLevelState, EndLevelType>(EndLevelType.FAIL);
        }
    }
}