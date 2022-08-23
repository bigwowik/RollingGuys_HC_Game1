using System;
using CodeBase.Infrastructure.Inputs;
using CodeBase.Logic.Friends;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Player
{
    public class Hero : MonoBehaviour
    {

        [SerializeField] private float ForwardSpeed = 1; // TODO Depends of friends count
        [SerializeField] private float HorizontalSpeed = 1;
        private Rigidbody _rigidbody;

        private IInputService _inputService;
        private bool _isInputActive;
        private float _velocityX;

        public bool CanMove { get; set; } = false;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

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
            _velocityX = _inputService.VelocityX;
            _isInputActive = _inputService.isActive;
        }

        private void FixedUpdate()
        {
            if (_isInputActive)
                Move(_velocityX);
        }

        public void Move(float velocityX)
        {
            _rigidbody.MovePosition(transform.position + (Vector3.forward * ForwardSpeed + Vector3.right * velocityX * HorizontalSpeed) * Time.deltaTime);
        }
    }
}