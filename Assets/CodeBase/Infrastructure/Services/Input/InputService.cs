using UnityEngine;

namespace CodeBase.Infrastructure.Services.Input
{
    public abstract class InputService : IInputService
    {
        
        



        public abstract Vector2 Axis { get; }
        public abstract Vector2 MouseAxis { get; }
        public abstract bool IsPauseButtonDown { get; }
        public abstract bool IsUseButtonDown { get; }

        public abstract bool IsAttackButtonDown();
        public abstract bool IsSprintButtonDown();
        public abstract bool IsJumpButtonDown();
        public abstract bool IsCrouchButtonDown();


    }
}