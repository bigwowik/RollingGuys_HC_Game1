using CodeBase.Infrastructure.Inputs;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Player
{
    public class Hero : MonoBehaviour
    {

        [SerializeField] private float Speed;

        private IInputService _inputService;

        [Inject]
        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        public void Move(Vector3 newPosition)
        {
            transform.position = newPosition;
        }

        private void Update()
        {
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
            transform.position += (Vector3.forward * Speed + Vector3.right * velocityX) * Time.deltaTime;
        }
    }
}