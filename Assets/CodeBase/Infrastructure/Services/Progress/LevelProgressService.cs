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

        private int _collectedCoins;

        public LevelProgressService(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;

            _collectedCoins = 0;
        }

        public void EndLevelTriggerAction(LevelResult levelResult)
        {
            _gameStateMachine.Enter<EndLevelState, LevelResult>(levelResult);
            
        }

        public void AddCoin(int coinCount)
        {
            _collectedCoins += coinCount;
        }

        public int GetAndResetCollectedCoins()
        {
            var coins = _collectedCoins;
            _collectedCoins = 0;
            return coins;
        }
        

        public void ReloadLevelWithFail()
        {
            _gameStateMachine.Enter<EndLevelState, LevelResult>(LevelResult.FAIL);
        }
        
    }
}