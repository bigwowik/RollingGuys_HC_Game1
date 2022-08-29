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
        private IFriend _friend;
        
        //variables
        private Vector3 _nextPosition;

        [Inject]
        public void Construct(IInputService inputService, FriendConfig friendConfig)
        {
            _friendConfig = friendConfig;
            _inputService = inputService;
        }

        private void Awake()
        {
            _friend = GetComponent<IFriend>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if(!_inputService.isActive) return;
            
            TrySetStartNextPosition();//once
            
            TryUpdateNextPosition();
            MovePositionToLerpNext();
        }

        private void MovePositionToLerpNext() => 
            _rigidbody.position = Vector3.Lerp(_rigidbody.position, _nextPosition, _friendConfig.Speed * Time.fixedDeltaTime);

        private void TryUpdateNextPosition()
        {
            if (DistanceGreaterCritical())
                _nextPosition = _friend.NextFriend.Position;
        }

        private bool DistanceGreaterCritical() => 
            Vector3.Distance(_friend.NextFriend.Position, _rigidbody.position) > _friendConfig.CriticalDistance;

        private void TrySetStartNextPosition()
        {
            if (_nextPosition == Vector3.zero)
                UpdateNextPosition();
        }

        private void UpdateNextPosition() => 
            _nextPosition = _friend.NextFriend.Position;
    }
    
    
}