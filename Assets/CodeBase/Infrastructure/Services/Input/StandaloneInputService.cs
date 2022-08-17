using UnityEngine;

namespace CodeBase.Infrastructure.Services.Input
{
    public class StandaloneInputService : InputService
    {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";
        protected const string MouseX = "Mouse X";
        protected const string MouseY = "Mouse Y";
        protected const string AttackButton = "Fire1";
        protected const string SprintButton = "Sprint";
        protected const string JumpButton = "Jump";
        protected const string CrouchButton = "Crouch";
        protected const string PauseButton = "Cancel";
        protected const string UseButton = "Use";
        
        public override Vector2 Axis => 
            UnityAxis();

        public override Vector2 MouseAxis => 
            UnityMouseAxis();

        public override bool IsPauseButtonDown =>
            UnityEngine.Input.GetButtonDown(PauseButton);
        public override bool IsUseButtonDown  => 
            UnityEngine.Input.GetButtonDown(UseButton);

        public override bool IsAttackButtonDown() => 
            UnityEngine.Input.GetButtonDown(AttackButton);

        public override bool IsSprintButtonDown() =>
            UnityEngine.Input.GetButton(SprintButton);
        public override bool IsJumpButtonDown() => 
            UnityEngine.Input.GetButtonDown(JumpButton);
        public override bool IsCrouchButtonDown() => 
            UnityEngine.Input.GetButtonDown(CrouchButton);
        
        private Vector2 UnityAxis() => 
            new Vector2(UnityEngine.Input.GetAxis(Horizontal), UnityEngine.Input.GetAxis(Vertical));

        private Vector2 UnityMouseAxis() =>
            new Vector2(UnityEngine.Input.GetAxis(MouseX), UnityEngine.Input.GetAxis(MouseY));
    }
}