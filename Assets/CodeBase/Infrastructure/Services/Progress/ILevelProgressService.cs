using Cinemachine;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface ILevelProgressService
    {
        Transform ActivePlayer { get; set; }
        CinemachineVirtualCamera PlayerCamera { get; set; }
        void ReloadLevel();
    }
}