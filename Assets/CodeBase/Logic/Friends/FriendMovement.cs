using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Inputs;
using CodeBase.Logic.Player;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Friends
{
    public class FriendMovement : MonoBehaviour
    {
        //dependencies
        private IInputService _inputService;
        private FriendConfig _friendConfig;

        //components
        private Rigidbody _rigidbody;
        private FriendChain _friend;
        
        //variables
        private Vector3 _nextPosition;
        private bool Moving { get; set; }

        [Inject]
        public void Construct(IInputService inputService, FriendConfig friendConfig)
        {
            _friendConfig = friendConfig;
            _inputService = inputService;
        }

        private void Awake()
        {
            _friend = GetComponent<FriendChain>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            Moving = _inputService.isActive;
        }

        private void FixedUpdate()
        {
            if(!Moving) return;
            
            TrySetStartNextPosition();//once
            
            TryUpdateNextPosition();
            MovePositionToLerpNext();
        }

        public void MovePositionToLerpNext()
        {
            var newPos =
                Vector3.Lerp(_rigidbody.position, _nextPosition, _friendConfig.Speed * Time.fixedDeltaTime );
             
            _rigidbody.MovePosition(newPos);
        }

        private void TryUpdateNextPosition()
        {
            if (DistanceGreaterCritical())
                _nextPosition = _friend.Next.Position;
        }

        private bool DistanceGreaterCritical() => 
            Vector3.Distance(_friend.Next.Position, _rigidbody.position) > _friendConfig.CriticalDistance;

        private void TrySetStartNextPosition()
        {
            if (_nextPosition == Vector3.zero)
                UpdateNextPosition();
        }

        private void UpdateNextPosition() => 
            _nextPosition = _friend.Next.Position;
    }
    
    
}