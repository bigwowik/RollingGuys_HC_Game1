using UnityEngine;

namespace CodeBase.Infrastructure.Inputs
{
    public class UnityInputService : IInputService
    {
        public bool isBlocked { get; set; }= false;
        public bool isActive => !isBlocked && Input.GetAxisRaw("Vertical") != 0;

        public float VelocityX => Input.GetAxisRaw("Horizontal");
    }
    
    public class MouseInputService : IInputService
    {
        public bool isBlocked { get; set; } = false;
        public bool isActive =>  !isBlocked && Input.GetMouseButton(0);

        public float VelocityX => Input.GetAxis("Mouse X");
    }
    
    public class TouchInputService : IInputService
    {
        public bool isBlocked { get; set; }= false;
        public bool isActive => !isBlocked && Input.touchCount > 0;

        public float VelocityX => 0;
    }
}
    