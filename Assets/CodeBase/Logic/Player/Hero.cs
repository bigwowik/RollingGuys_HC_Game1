using CodeBase.Infrastructure.Inputs;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Player
{
    public class Hero : MonoBehaviour
    {

        [SerializeField] private float ForwardSpeed = 1;
        [SerializeField] private float HorizontalSpeed = 1;

        private IInputService _inputService;

        public bool CanMove { get; set; } = false;
        
        [Inject]
        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        

        private void Update()
        {
            if(CanMove)
                InputUpdateHandle();
        }

        private void InputUpdateHandle()
        {
            var velocityX = _inputService.VelocityX;
            var isInputActive = _inputService.isActive;

            if (isInputActive)
                Move(velocityX);
        }

        public void Move(float velocityX)
        {
            transform.position += (Vector3.forward * ForwardSpeed + Vector3.right * velocityX * HorizontalSpeed) * Time.deltaTime;
        }
    }
}