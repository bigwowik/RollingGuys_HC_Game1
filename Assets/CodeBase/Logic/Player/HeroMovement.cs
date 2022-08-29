using System;
using CodeBase.Infrastructure.Inputs;
using CodeBase.Logic.Friends;
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
            if (_inputService.isActive)
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