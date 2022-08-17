using UnityEngine;

namespace CodeBase.Infrastructure.Services.Input
{
    public interface IInputService : IService
    {
        Vector2 Axis { get; }
        Vector2 MouseAxis { get; }
        bool IsPauseButtonDown { get; }
        bool IsUseButtonDown { get; }

        bool IsAttackButtonDown();
        bool IsSprintButtonDown();
        bool IsJumpButtonDown();
        bool IsCrouchButtonDown();



    }
}