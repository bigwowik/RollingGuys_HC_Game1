using System;
using Cinemachine;
using CodeBase.Infrastructure.States;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface ILevelProgressService
    {
        Transform ActivePlayer { get; set; }
        CinemachineVirtualCamera PlayerCamera { get; set; }
        void ReloadLevelWithFail();
        void EndLevelTriggerAction(LevelResult levelResult);
        void AddCoin(int coinCount);
        int GetAndResetCollectedCoins();
        event Action OnLevelStarted;
        void StartLevel();
        event Action OnLevelWin;
    }
}