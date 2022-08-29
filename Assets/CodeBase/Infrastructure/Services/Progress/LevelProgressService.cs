using Cinemachine;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.States;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Progress
{
    public class LevelProgressService : ILevelProgressService
    {
        private readonly IGameStateMachine _gameStateMachine;

        public LevelProgressService(IGameStateMachine gameStateMachine) => 
            _gameStateMachine = gameStateMachine;

        public Transform ActivePlayer { get; set; }
        public CinemachineVirtualCamera PlayerCamera { get; set; }

        public void ReloadLevel() => 
            _gameStateMachine.Enter<EndLevelState, EndLevelType>(EndLevelType.FAIL);
    }
}