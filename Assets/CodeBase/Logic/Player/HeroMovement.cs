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

        private float _speed;
        private float _lerpMaxSpeed;
        
        [Inject]
        public void Construct(IInputService inputService, HeroConfig heroConfig)
        {
            _inputService = inputService;
            _heroConfig = heroConfig;
        }

        private void Awake() => 
            _rigidbody = GetComponent<Rigidbody>();

        private void FixedUpdate()
        { 
            _lerpMaxSpeed = _inputService.isActive ? 1f : 0f;
            _speed = Mathf.Lerp(_speed, _lerpMaxSpeed, _heroConfig.Acceleration * Time.fixedDeltaTime);
            Move(_speed);
        }

        private void Move(float lerpSpeed)
        {
            _rigidbody.MovePosition(_rigidbody.position + 
                                    (Vector3.forward * _heroConfig.ForwardSpeed + 
                                     Vector3.right * _inputService.VelocityX * _heroConfig.HorizontalSpeed) 
                                    * lerpSpeed 
                                    * Time.fixedDeltaTime);
        }
    }
    
    
    
}