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

        private IInputService _inputService;
        private FriendConfig _friendConfig;

        private Rigidbody _rigidbody;
        private IFriend _friend;
        
        private Vector3 _lastNextFriendPosition;

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
            
            if (Vector3.Distance(_friend.NextFriend.Position, _rigidbody.position) > _friendConfig.CriticalDistance)
                _lastNextFriendPosition = _friend.NextFriend.Position;

            _rigidbody.position = Vector3.Lerp(_rigidbody.position, _lastNextFriendPosition, _friendConfig.Speed * Time.fixedDeltaTime);
        }
    }
    
    
}