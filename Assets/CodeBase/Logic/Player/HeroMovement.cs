using System;
using CodeBase.Infrastructure.Inputs;
using CodeBase.Logic.Friends;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Player
{
    public class HeroMovement : MonoBehaviour
    {
        //dependencies
        private IInputService _inputService;
        private HeroConfig _heroConfig;
        
        //components
        private Rigidbody _rigidbody;

        private bool IsMoving;
        private float _lerpMaxSpeed;

        private bool skipOneFrame;
        
        [Inject]
        public void Construct(IInputService inputService, HeroConfig heroConfig)
        {
            _inputService = inputService;
            _heroConfig = heroConfig;
        }

        private void Awake() => 
            _rigidbody = GetComponent<Rigidbody>();

        private void Update()
        {
            if (_inputService.isActive)
            {
                if (TrySkipOneFrame()) return;

                IsMoving = true;
            }
            //_speed = Mathf.Lerp(_speed, 1, _heroConfig.Acceleration * Time.deltaTime);
            else
            {
                IsMoving = false;
            }
        }

        private bool TrySkipOneFrame()
        {
            if (!skipOneFrame)
            {
                skipOneFrame = true;
                return true;
            }

            return false;
        }

        private void FixedUpdate()
        {
            if(!IsMoving) return;
            Move();
        }

        private void Move()
        {
            _rigidbody.MovePosition(_rigidbody.position + 
                                    (Vector3.forward * _heroConfig.ForwardSpeed + 
                                     Vector3.right * _inputService.VelocityX * _heroConfig.HorizontalSpeed)
                                    * Time.fixedDeltaTime);
        }
    }
    
    
    
}