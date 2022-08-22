using UnityEngine;

namespace CodeBase.Infrastructure.Inputs
{
    public class UnityInputService : IInputService
    {
        public Vector3 Axis => new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);

        public bool isActive => Input.GetAxisRaw("Vertical") != 0;

        public float VelocityX => Input.GetAxisRaw("Horizontal");
    }
}
    