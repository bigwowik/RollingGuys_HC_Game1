using UnityEngine;

namespace CodeBase.Infrastructure.Inputs
{
    public class UnityInputService : IInputService
    {
        public Vector3 Axis => new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);

        public bool isActive => Input.GetAxisRaw("Vertical") != 0;

        public float VelocityX => Input.GetAxisRaw("Horizontal");
    }
    
    public class MouseInputService : IInputService
    {
        
        public bool isActive => Input.GetMouseButton(0);

        public float VelocityX => Input.GetAxis("Mouse X");
    }
    
    public class TouchInputService : IInputService
    {
        
        public bool isActive => Input.touchCount > 0;

        public float VelocityX => 0;
    }
}
    